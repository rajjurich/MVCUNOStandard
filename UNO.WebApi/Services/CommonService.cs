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
    public interface ICommonService
    {
        Task<int> Create(Common entity, string ipaddress, int activeuser);
        Task<int> Edit(Common entity, string ipaddress, int activeuser);
        Task<int> Delete(int Id, string ipaddress, int activeuser);
        List<Common> GetCommon(int id);
        List<Common> GetCommonWithNoAccess(int id, string types);
        Common GetCommonSingle(int id);
        Task<bool> IsUniqOCECode(Common entity, bool isEdit);

        List<FilterModel> GetFilterValues(string id, int activeuser, string conditions);

  
    }
    public class CommonService : ICommonService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;

        public CommonService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }

        public async Task<int> Create(Common entity, string ipaddress, int activeuser)
        {
            string query = " INSERT INTO ent_org_common_entities ( COMMON_TYPES_ID,OCE_ID,OCE_DESCRIPTION,COMPANY_ID,OCE_ISDELETED) "
                            + " VALUES ('" + entity.COMMON_TYPES_ID + "', '" + entity.OCE_ID + "', '" + entity.OCE_DESCRIPTION + "', '" + entity.Company_ID + "', 0)";

            query += " select @@Identity";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(Common entity, string ipaddress, int activeuser)
        {
            //string query = " UPDATE ent_org_common_entities SET OCE_ID = '" + entity.OCE_ID + "' , OCE_DESCRIPTION ='" + entity.OCE_DESCRIPTION + "' , COMMON_TYPES_ID ='" + entity.COMMON_TYPES_ID + "' , Company_ID ='" + entity.Company_ID + "' WHERE ID =  " + entity.ID;
            string query = " UPDATE ent_org_common_entities SET OCE_ID = '" + entity.OCE_ID + "' , OCE_DESCRIPTION ='" + entity.OCE_DESCRIPTION + "'  WHERE ID =  " + entity.ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int Id, string ipaddress, int activeuser)
        {
            string query = "UPDATE ent_org_common_entities SET OCE_ISDELETED = 1  WHERE ID = " + Id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<Common> GetCommon(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " SELECT ID,(select common_types From ent_org_common_types where COMMON_TYPES_ID=common_entities.COMMON_TYPES_ID)as COMMON_NAME, COMMON_TYPES_ID, OCE_ID, OCE_DESCRIPTION,(select COMPANY_NAME From ENT_COMPANY where company_id=common_entities.company_id)as Company_Name,Company_ID FROM ent_org_common_entities common_entities WHERE OCE_ISDELETED =0 ";
            }
            else
            {
                query = " SELECT ID,(select common_types From ent_org_common_types where COMMON_TYPES_ID=common_entities.COMMON_TYPES_ID)as COMMON_NAME, COMMON_TYPES_ID, OCE_ID, OCE_DESCRIPTION,(select COMPANY_NAME From ENT_COMPANY where company_id=common_entities.company_id)as Company_Name,Company_ID FROM ent_org_common_entities common_entities WHERE OCE_ISDELETED =0 and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";

                query += " union " +
                         " select ID,COMMON_TYPES as COMMON_NAME, ct.COMMON_TYPES_ID, OCE_ID,OCE_DESCRIPTION,com.COMPANY_NAME,com.Company_ID from ENT_ROLE_DATA_ACCESS da  " +
                         " join ent_org_common_types ct on da.COMMON_TYPES_ID=ct.COMMON_TYPES_ID " +
                         " join ent_org_common_entities org on org.ID=MAPPED_ENTITY_ID and ct.COMMON_TYPES<>'Company' and ct.COMMON_TYPES<>'Employee' " +
                         " join ent_company com on com.COMPANY_ID=org.COMPANY_ID and da.MAPPED_ENTITY_ID=org.ID " +
                         " where da.USER_CODE=" + id + "";
            }

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Common> entParamsList = new List<Common>();

            foreach (DataRow dr in x.Rows)
            {
                Common entity = new Common();
                entity.ID = Convert.ToInt32(dr["ID"]);
                entity.COMMON_TYPES_ID = Convert.ToInt32(dr["COMMON_TYPES_ID"]);
                entity.COMMON_NAME = Convert.ToString(dr["COMMON_NAME"]);
                entity.OCE_ID = Convert.ToString(dr["OCE_ID"]);
                entity.OCE_DESCRIPTION = Convert.ToString(dr["OCE_DESCRIPTION"]);
                entity.Company_ID = Convert.ToInt32(dr["COMMON_TYPES_ID"]);
                entity.Company_Name = Convert.ToString(dr["Company_Name"]);
                entParamsList.Add(entity);
            }
            return entParamsList;
        }

        public List<Common> GetCommonWithNoAccess(int id, string types)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " SELECT ID,(select common_types From ent_org_common_types where COMMON_TYPES_ID=common_entities.COMMON_TYPES_ID)as COMMON_NAME, COMMON_TYPES_ID, OCE_ID, OCE_DESCRIPTION,(select COMPANY_NAME From ENT_COMPANY where company_id=common_entities.company_id)as Company_Name,Company_ID FROM ent_org_common_entities common_entities WHERE OCE_ISDELETED =0 and id not in (select distinct(isnull(ENTITY_ID,0)) from EAL_CONFIG where ENTITY_TYPE in  (select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='" + types + "') and ISDELETED='0')";
            }
            else
            {
                query = " SELECT ID,(select common_types From ent_org_common_types where COMMON_TYPES_ID=common_entities.COMMON_TYPES_ID)as COMMON_NAME, COMMON_TYPES_ID, OCE_ID, OCE_DESCRIPTION,(select COMPANY_NAME From ENT_COMPANY where company_id=common_entities.company_id)as Company_Name,Company_ID FROM ent_org_common_entities common_entities WHERE OCE_ISDELETED =0 and id not in (select distinct(isnull(ENTITY_ID,0)) from EAL_CONFIG where ENTITY_TYPE in  (select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='" + types + "') and ISDELETED='0') and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";

                query += " union " +
                         " select ID,COMMON_TYPES as COMMON_NAME, ct.COMMON_TYPES_ID, OCE_ID,OCE_DESCRIPTION,com.COMPANY_NAME,com.Company_ID from ENT_ROLE_DATA_ACCESS da  " +
                         " join ent_org_common_types ct on da.COMMON_TYPES_ID=ct.COMMON_TYPES_ID " +
                         " join ent_org_common_entities org on org.ID=MAPPED_ENTITY_ID and ct.COMMON_TYPES<>'Company' and ct.COMMON_TYPES<>'Employee' " +
                         " join ent_company com on com.COMPANY_ID=org.COMPANY_ID and da.MAPPED_ENTITY_ID=org.ID " +
                         " where da.USER_CODE=" + id + "";
            }

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Common> entParamsList = new List<Common>();

            foreach (DataRow dr in x.Rows)
            {
                Common entity = new Common();
                entity.ID = Convert.ToInt32(dr["ID"]);
                entity.COMMON_TYPES_ID = Convert.ToInt32(dr["COMMON_TYPES_ID"]);
                entity.COMMON_NAME = Convert.ToString(dr["COMMON_NAME"]);
                entity.OCE_ID = Convert.ToString(dr["OCE_ID"]);
                entity.OCE_DESCRIPTION = Convert.ToString(dr["OCE_DESCRIPTION"]);
                entity.Company_ID = Convert.ToInt32(dr["COMMON_TYPES_ID"]);
                entity.Company_Name = Convert.ToString(dr["Company_Name"]);
                entParamsList.Add(entity);
            }
            return entParamsList;
        }

        public Common GetCommonSingle(int id)
        {
            string query = " SELECT ID,(select common_types From ent_org_common_types where COMMON_TYPES_ID=common_entities.COMMON_TYPES_ID)as COMMON_NAME, COMMON_TYPES_ID, OCE_ID, OCE_DESCRIPTION, (select COMPANY_NAME From ENT_COMPANY where company_id=common_entities.company_id)as Company_Name,Company_ID FROM ent_org_common_entities common_entities WHERE OCE_ISDELETED =0 and ID = " + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            Common entity = new Common();

            foreach (DataRow dr in x.Rows)
            {
                entity.ID = Convert.ToInt32(dr["ID"]);
                entity.COMMON_TYPES_ID = Convert.ToInt32(dr["COMMON_TYPES_ID"]);
                entity.COMMON_NAME = Convert.ToString(dr["COMMON_NAME"]);
                entity.OCE_ID = Convert.ToString(dr["OCE_ID"]);
                entity.OCE_DESCRIPTION = Convert.ToString(dr["OCE_DESCRIPTION"]);
                entity.Company_ID = Convert.ToInt32(dr["Company_ID"]);
                entity.Company_Name = Convert.ToString(dr["Company_Name"]);
            }
            return entity;
        }

        public async Task<bool> IsUniqOCECode(Common entity, bool isEdit)
        {
            string query = "SELECT count(OCE_ID) FROM ent_org_common_entities WHERE OCE_ISDELETED =0 and OCE_ID = '" + entity.OCE_ID + "' and COMPANY_ID =" + entity.Company_ID + " and COMMON_TYPES_ID=" + entity.COMMON_TYPES_ID;
            if (isEdit)
                query = query + " and ID<>" + entity.ID;

            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text) == 0 ? false : true;
            return x;

        }


        public List<FilterModel> GetFilterValues(string id, int activeuser, string conditions)
        {
            string query = "";
            User user = _userService.GetSingleUser(activeuser);
            if (user.USER_CODE == "superuser")
            {
                query = " select distinct ec.COMPANY_NAME,ec.COMPANY_ID" +
                 " ,CAT.COMMON_TYPES_ID " +
                 " ,CAT.ID catid,CAT.OCE_ID +'-'+CAT.OCE_DESCRIPTION catdes " +
                 " ,LOC.ID locid,LOC.OCE_ID +'-'+LOC.OCE_DESCRIPTION locdesc " +
                 " ,GRD.ID grdid,GRD.OCE_ID +'-'+GRD.OCE_DESCRIPTION grddesc " +
                 " ,GRP.ID grpid,GRP.OCE_ID +'-'+GRP.OCE_DESCRIPTION grpdesc " +
                 " ,DIV.ID divid,DIV.OCE_ID+'-'+DIV.OCE_DESCRIPTION divdesc " +
                 " ,DEPT.ID deptid,DEPT.OCE_ID+'-'+DEPT.OCE_DESCRIPTION deptdesc" +
                 " ,DESG.ID desgid,DESG.OCE_ID+'-'+DESG.OCE_DESCRIPTION desgdesc " +
                 " from ent_company ec " +

                 " left join ENT_ORG_COMMON_ENTITIES CAT on CAT.COMPANY_ID=ec.COMPANY_ID and " +
                 " CAT.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where " +
                 " COMMON_TYPES='CATEGORY') " +

                 " left join ENT_ORG_COMMON_ENTITIES LOC on LOC.COMPANY_ID=ec.COMPANY_ID and LOC.COMMON_TYPES_ID=(" +
                 " select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='LOCATION')  " +

                 " left join ENT_ORG_COMMON_ENTITIES GRD on GRD.COMPANY_ID=ec.COMPANY_ID " +
                 " and GRD.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES " +
                 " where COMMON_TYPES='GRADE') " +

                 " left join ENT_ORG_COMMON_ENTITIES GRP on " +
                 " GRP.COMPANY_ID=ec.COMPANY_ID and GRP.COMMON_TYPES_ID=" +
                 " (select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='GROUP') " +

                 " left join ENT_ORG_COMMON_ENTITIES DIV on DIV.COMPANY_ID=ec.COMPANY_ID " +
                 " and DIV.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES " +
                 " where COMMON_TYPES='DIVISION') " +

                 " left join ENT_ORG_COMMON_ENTITIES DEPT on " +
                 " DEPT.COMPANY_ID=ec.COMPANY_ID and DEPT.COMMON_TYPES_ID=(select COMMON_TYPES_ID " +
                 " from ENT_ORG_COMMON_TYPES where COMMON_TYPES='DEPARTMENT') " +

                 " left join " +
                 " ENT_ORG_COMMON_ENTITIES DESG on DESG.COMPANY_ID=ec.COMPANY_ID and " +
                 " DESG.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where " +
                 " COMMON_TYPES='DESIGNATION')  " +
                 " where  "+ conditions + " in(" + id + ")";
            }
            else
            {
                query = " select distinct ec.COMPANY_NAME,ec.COMPANY_ID" +
                 " ,CAT.COMMON_TYPES_ID " +
                 " ,CAT.ID catid,CAT.OCE_ID +'-'+CAT.OCE_DESCRIPTION catdes " +
                 " ,LOC.ID locid,LOC.OCE_ID +'-'+LOC.OCE_DESCRIPTION locdesc " +
                 " ,GRD.ID grdid,GRD.OCE_ID +'-'+GRD.OCE_DESCRIPTION grddesc " +
                 " ,GRP.ID grpid,GRP.OCE_ID +'-'+GRP.OCE_DESCRIPTION grpdesc " +
                 " ,DIV.ID divid,DIV.OCE_ID+'-'+DIV.OCE_DESCRIPTION divdesc " +
                 " ,DEPT.ID deptid,DEPT.OCE_ID+'-'+DEPT.OCE_DESCRIPTION deptdesc" +
                 " ,DESG.ID desgid,DESG.OCE_ID+'-'+DESG.OCE_DESCRIPTION desgdesc " +
                 " from ent_company ec " +

                 " left join ENT_ORG_COMMON_ENTITIES CAT on CAT.COMPANY_ID=ec.COMPANY_ID and " +
                 " CAT.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where " +
                 " COMMON_TYPES='CATEGORY') " +

                 " left join ENT_ORG_COMMON_ENTITIES LOC on LOC.COMPANY_ID=ec.COMPANY_ID and LOC.COMMON_TYPES_ID=(" +
                 " select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='LOCATION')  " +

                 " left join ENT_ORG_COMMON_ENTITIES GRD on GRD.COMPANY_ID=ec.COMPANY_ID " +
                 " and GRD.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES " +
                 " where COMMON_TYPES='GRADE') " +

                 " left join ENT_ORG_COMMON_ENTITIES GRP on " +
                 " GRP.COMPANY_ID=ec.COMPANY_ID and GRP.COMMON_TYPES_ID=" +
                 " (select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where COMMON_TYPES='GROUP') " +

                 " left join ENT_ORG_COMMON_ENTITIES DIV on DIV.COMPANY_ID=ec.COMPANY_ID " +
                 " and DIV.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES " +
                 " where COMMON_TYPES='DIVISION') " +

                 " left join ENT_ORG_COMMON_ENTITIES DEPT on " +
                 " DEPT.COMPANY_ID=ec.COMPANY_ID and DEPT.COMMON_TYPES_ID=(select COMMON_TYPES_ID " +
                 " from ENT_ORG_COMMON_TYPES where COMMON_TYPES='DEPARTMENT') " +

                 " left join " +
                 " ENT_ORG_COMMON_ENTITIES DESG on DESG.COMPANY_ID=ec.COMPANY_ID and " +
                 " DESG.COMMON_TYPES_ID=(select COMMON_TYPES_ID from ENT_ORG_COMMON_TYPES where " +
                 " COMMON_TYPES='DESIGNATION')  " +
                 " where  " + conditions + " in(" + id + ")";
            }
            

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            List<FilterModel> filterlist = new List<FilterModel>();

            foreach (DataRow dr in x.Rows)
            {
                int companyid = 1;
                int catid = 1;
                int locid = 1;
                int grdid = 1;
                int grpid = 1;
                int divid = 1;
                int deptid = 1;
                int desgid = 1;
                if (string.IsNullOrEmpty(dr["COMPANY_ID"].ToString()))
                {
                    companyid = 0;
                }
                else { companyid = Convert.ToInt16(dr["COMPANY_ID"]); }
                if (string.IsNullOrEmpty(dr["catid"].ToString()))
                {
                    catid = 0;
                }
                else { catid = Convert.ToInt16(dr["catid"]); }
                if (string.IsNullOrEmpty(dr["locid"].ToString()))
                {
                    locid = 0;
                }
                else { locid = Convert.ToInt16(dr["locid"]); }
                if (string.IsNullOrEmpty(dr["grdid"].ToString()))
                {
                    grdid = 0;
                }
                else { grdid = Convert.ToInt16(dr["grdid"]); }
                if (string.IsNullOrEmpty(dr["grpid"].ToString()))
                {
                    grpid= 0;
                }
                else { grpid = Convert.ToInt16(dr["grpid"]); }
                if (string.IsNullOrEmpty(dr["divid"].ToString()))
                {
                    divid= 0;
                }
                else { divid = Convert.ToInt16(dr["divid"]); }
                if (string.IsNullOrEmpty(dr["deptid"].ToString()))
                {
                    deptid = 0;
                }
                else { deptid = Convert.ToInt16(dr["deptid"]); }
                if (string.IsNullOrEmpty(dr["desgid"].ToString()))
                {
                    desgid= 0;
                }
                else { desgid = Convert.ToInt16(dr["desgid"]); }

                FilterModel f = new FilterModel();
                f.Companyid = companyid;
                f.CompanyName = Convert.ToString(dr["COMPANY_NAME"]);
                f.Categoryid = catid;
                f.CategoryName = Convert.ToString(dr["catdes"]);
                f.Locationid = locid;
                f.LocationName = Convert.ToString(dr["locdesc"]);
                f.Gradeid = grdid;
                f.GradeName = Convert.ToString(dr["grddesc"]);
                f.Groupid = grpid;
                f.GroupName = Convert.ToString(dr["grpdesc"]);
                f.Divid = divid;
                f.DivName = Convert.ToString(dr["divdesc"]);
                f.Departmentid = deptid;
                f.DepartmentName = Convert.ToString(dr["deptdesc"]);
                f.Designationid = desgid;
                f.DesignationName = Convert.ToString(dr["desgdesc"]);
                filterlist.Add(f);
            }


            return filterlist;
        }

       

    
    }
}