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

    public interface IRolemasterService
    {
        List<ROLEMASTERMODEL> GetRolemastersDetails(int id);

        ROLEMASTERMODEL GetRolemaster(int id);

        Task<int> Create(ROLEMASTERMODEL entity, string ipaddress, int activeuser);

        Task<int> Edit(ROLEMASTERMODEL entity, string ipaddress, int activeuser);

        Task<int> Delete(int id, string ipaddress, int activeuser);
    }
    public class RolemasterService : IRolemasterService
    {

        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;



        public RolemasterService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }

        public List<ROLEMASTERMODEL> GetRolemastersDetails(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = "select rm.ROLE_ID,rm.ROLE_NAME,ec.COMPANY_NAME from ENT_ROLE_MASTER rm inner join ent_company ec on rm.COMPANY_ID=ec.COMPANY_ID where ROLE_NAME not in('ADMIN','EMPLOYEE','SYSADMIN')  and ROLE_IsDeleted=0";
            }
            else
            {
                query = "select rm.ROLE_ID,rm.ROLE_NAME,ec.COMPANY_NAME from ENT_ROLE_MASTER rm inner join ent_company ec on rm.COMPANY_ID=ec.COMPANY_ID where rm.COMPANY_ID in(" + CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) and ROLE_NAME not in('ADMIN','EMPLOYEE','SYSADMIN') and ROLE_IsDeleted=0";
            }
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ROLEMASTERMODEL> rolemasterlist = new List<ROLEMASTERMODEL>();

            foreach(DataRow dr in x.Rows)
            {
                ROLEMASTERMODEL rolemasterobj = new ROLEMASTERMODEL();
                rolemasterobj.rolemasterid = Convert.ToInt32(dr["ROLE_ID"]);
                rolemasterobj.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                rolemasterobj.Company_name = Convert.ToString(dr["COMPANY_NAME"]);
                rolemasterlist.Add(rolemasterobj);
            }
            return rolemasterlist;
        }

        public ROLEMASTERMODEL GetRolemaster(int id)
        {
            string query = "select rm.ROLE_ID,rm.ROLE_NAME,ec.COMPANY_NAME from ENT_ROLE_MASTER rm inner join ent_company ec on rm.COMPANY_ID=ec.COMPANY_ID where rm.ROLE_ID in(" + id + ") and ROLE_NAME not in('ADMIN','EMPLOYEE','SYSADMIN')";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            ROLEMASTERMODEL rolemasterobj = new ROLEMASTERMODEL();

            foreach(DataRow dr in x.Rows)
            {
                rolemasterobj.rolemasterid = Convert.ToInt32(dr["ROLE_ID"]);
                rolemasterobj.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                rolemasterobj.Company_name = Convert.ToString(dr["COMPANY_NAME"]);
            }

            return rolemasterobj;
        }

        public async Task<int> Create(ROLEMASTERMODEL entity, string ipaddress, int activeuser)
        {
            string query = "";
            
            
            query = "insert into ENT_ROLE_MASTER values( '" + entity.ROLE_NAME + "',0,null," + entity.Company_name + ",0)";
            
                
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(ROLEMASTERMODEL entity, string ipaddress, int activeuser)
        {
            string query = "";

            query = "update ENT_ROLE_MASTER set  ROLE_NAME='" + entity.ROLE_NAME + "'  ,COMPANY_ID=" + entity.Company_name + " where ROLE_ID=" + entity.rolemasterid;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "";

            query = "update ENT_ROLE_MASTER set  ROLE_IsDeleted=1 , ROLE_DeletedDate='"+DateTime.Now+"'  where ROLE_ID="+id+" ";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}