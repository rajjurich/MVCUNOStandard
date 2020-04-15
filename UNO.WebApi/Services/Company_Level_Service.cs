using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface ICompany_Level_Service
    {
        List<Company_Access_APIModel> GetCompanyAccessList();

        Company_Access_APIModel GetCompanyAccessOne(int id);

        List<Company_Access_APIModel> GetListForMenuAccess(int id);

        Task Create(Company_Access_APIModel entity, string ipaddress, int activeuser);


    }
    public class Company_Level_Service : ICompany_Level_Service
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public Company_Level_Service(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<Company_Access_APIModel> GetCompanyAccessList()
        {
            string query = "select mr.COMPANY_ID,ec.COMPANY_NAME "
                            + " from MenuCompanyRelation mr inner join ENT_COMPANY ec on mr.COMPANY_ID=ec.COMPANY_ID "
                            + " group by ec.COMPANY_NAME,mr.COMPANY_ID ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            List<Company_Access_APIModel> company_model_list = new List<Company_Access_APIModel>();

            foreach (DataRow dr in x.Rows)
            {
                Company_Access_APIModel companymodel = new Company_Access_APIModel();
                companymodel.Companyid = Convert.ToInt32(dr["COMPANY_ID"]);
                companymodel.CompanyName = Convert.ToString(dr["COMPANY_NAME"]);
                company_model_list.Add(companymodel);
            }

            return company_model_list;
        }

        public Company_Access_APIModel GetCompanyAccessOne(int id)
        {
            string query = "select mr.COMPANY_ID,ec.COMPANY_NAME"
                            + " from MenuCompanyRelation mr inner join ENT_COMPANY ec on mr.COMPANY_ID=ec.COMPANY_ID"
                            + " where mr.COMPANY_ID=" + id
                            + " group by ec.COMPANY_NAME,mr.COMPANY_ID  ";

            var d = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            Company_Access_APIModel companymodel = new Company_Access_APIModel();

            foreach (DataRow dr in d.Rows)
            {
                companymodel.Companyid = Convert.ToInt32(dr["COMPANY_ID"]);
                companymodel.CompanyName = Convert.ToString(dr["COMPANY_NAME"]);
            }
            return companymodel;
        }

        public List<Company_Access_APIModel> GetListForMenuAccess(int id)
        {
            string query = "select distinct b.COMPANY_ID,b.COMPANY_NAME,b.MENU_ID,b.MENU_NAME,b.MODULE_ID, "
                           + " b.MODULE_NAME,b.SMODULE_ID,b.SMODULE_NAME,ISNULL(rel.RelationId, 0 ) as RelationId"
                           + " from menucompanyrelation rel "
                           + " right join ("
                           + " select a.*, company_id,COMPANY_NAME,COMPANY_ISDELETED from ent_company,("
                           + " select mn.MENU_ID,mn.MENU_NAME,sm.SMODULE_ID,sm.SMODULE_NAME,mm.MODULE_ID,mm.MODULE_NAME"
                           + " from ENT_SUB_MODULE_MASTER as sm "
                           + " inner join  ENT_MODULE_MASTER as mm on mm.MODULE_ID=sm.MODULE_ID "
                           + " inner join ENT_MENU_MASTER as mn on mn.SMODULE_ID=sm.SMODULE_ID "
                           + " left join menucompanyrelation rel on rel.MENU_ID=mn.MENU_ID"
                           + " where mn.MENU_IsDeleted=0)a  where COMPANY_ISDELETED=0"
                           + " )b on b.company_id=rel.company_id and b.MENU_ID=rel.MENU_ID"
                           + " where b.company_id="+id
                           + " order by b.company_id,b.MENU_ID";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            List<Company_Access_APIModel> company_model_list = new List<Company_Access_APIModel>();

            foreach (DataRow dr in x.Rows)
            {
                Company_Access_APIModel companyobj = new Company_Access_APIModel();
                companyobj.Companyid = Convert.ToInt32(dr["COMPANY_ID"]);
                companyobj.CompanyName = Convert.ToString(dr["COMPANY_NAME"]);
                List<ModuleMenu> modmenu = new List<ModuleMenu>();
                ModuleMenu moduleobj = new ModuleMenu();
                List<SubMenu> submenulist = new List<SubMenu>();
                moduleobj.ModuleMenuId = Convert.ToInt32(dr["MODULE_ID"]);
                moduleobj.ModuleMenuName = Convert.ToString(dr["MODULE_NAME"]);
                modmenu.Add(moduleobj);
                moduleobj.SubMesnus = submenulist;
                SubMenu submenuobj = new SubMenu();
                List<menutable> menutablelist = new List<menutable>();
                submenuobj.SubMenuId = Convert.ToInt32(dr["SMODULE_ID"]);
                submenuobj.SubMenuName = Convert.ToString(dr["SMODULE_NAME"]);
                submenulist.Add(submenuobj);
                submenuobj.MenuNames = menutablelist;
                menutable menuobjin = new menutable();
                menuobjin.MenuId = Convert.ToInt32(dr["MENU_ID"]);
                menuobjin.MenuName = Convert.ToString(dr["MENU_NAME"]);
                menuobjin.RelationsId = Convert.ToInt32(dr["RelationId"]);
                menutablelist.Add(menuobjin);
                companyobj.ModuleSeleted = modmenu;
                company_model_list.Add(companyobj);
            }
            
            return company_model_list;
        }

        public async Task Create(Company_Access_APIModel entity, string ipaddress, int activeuser)
        {
            string querymain = "";

            for (int i = 0; i < entity.Menulistfromweb.Count; i++)
            {
                if (entity.IsStatus[i].ToString() == "False" && entity.RelationIdfromweb[i] != 0)
                {
                    string query = "delete menucompanyrelation where RelationId="+entity.RelationIdfromweb[i]+"  ";
                    querymain = querymain + query;
                }
                else if (entity.RelationIdfromweb[i] == 0 && entity.IsStatus[i].ToString() == "True")
                {
                    string query = "insert into MenuCompanyRelation values("+entity.Menulistfromweb[i]+","+entity.Companyid+",null,null)  ";
                    querymain = querymain + query;
                }
            }
            await _DatabaseHelper.Insert(querymain, CommandType.Text, ipaddress, activeuser);
        }


    }
}