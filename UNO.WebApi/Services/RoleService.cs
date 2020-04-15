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

    public interface IRoleService
    {
        Task<int> Create(Role entity, string ipaddress, int activeuser);
        Task<int> Edit(Role entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
        List<Role> GetRole(int id);
        List<Role> GetRoleByCompanyId(int id);
        Role GetSingleRole(int id);
        Task<int> GetRoleIdByRoleName(string name);

    }

    public class RoleService : IRoleService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;

        public RoleService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }

        public async Task<int> Create(Role entity, string ipaddress, int activeuser)
        {

            string query = " insert into ent_role_master (role_name,company_id,is_sync,ROLE_IsDeleted) " +
                     " values ('" + entity.ROLE_NAME + "', " + entity.COMPANY_ID + ", 0, 0) ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(Role entity, string ipaddress, int activeuser)
        {

            string query = "Update ent_role_master set role_name = '" + entity.ROLE_NAME + "', company_id = " + entity.COMPANY_ID + " , IS_SYNC =0  where ROLE_ID =  " + entity.ROLE_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "Update ent_role_master set ROLE_IsDeleted = 0, IS_SYNC = 0, ROLE_DeletedDate='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "' where ROLE_ID =  " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<Role> GetRole(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select * from ent_role_master ";
            }
            else
            {
                query = " select * from ent_role_master where COMPANY_ID in( "+
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }            

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Role> entRoleList = new List<Role>();

            foreach (DataRow dr in x.Rows)
            {
                Role entRole = new Role();
                entRole.ROLE_ID = Convert.ToInt16(dr["ROLE_ID"]);
                entRole.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                entRole.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entRoleList.Add(entRole);
            }
            return entRoleList;

        }

        public Role GetSingleRole(int id)
        {
            string query = "select * from ent_role_master where ROLE_ID = " + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            Role entRole = new Role();

            foreach (DataRow dr in x.Rows)
            {
                entRole.ROLE_ID = Convert.ToInt16(dr["ROLE_ID"]);
                entRole.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                entRole.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);

            }
            return entRole;
        }


        public async Task<int> GetRoleIdByRoleName(string name)
        {
            string query = "select Role_id from ent_role_master where ROLE_NAME = '" + name + "'";

            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text);
            return x;
            //return entRole;
        }


        public List<Role> GetRoleByCompanyId(int id)
        {
            //string query = string.Empty;
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select * from ent_role_master where COMPANY_ID  = " + id + "";      
            }
            else
            {
                query = " select * from ent_role_master where COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) or ROLE_NAME!= 'SYSADMIN'";
            }            

            //query = " select * from ent_role_master where COMPANY_ID  = " + id + "";            

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Role> entRoleList = new List<Role>();

            foreach (DataRow dr in x.Rows)
            {
                Role entRole = new Role();
                entRole.ROLE_ID = Convert.ToInt16(dr["ROLE_ID"]);
                entRole.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                entRole.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entRoleList.Add(entRole);
            }
            return entRoleList;
        }
    }
}