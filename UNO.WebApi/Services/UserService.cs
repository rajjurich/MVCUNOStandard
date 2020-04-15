using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Helpers;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IUserService
    {

        Task<int> Create(User entity, string ipaddress, int activeuser);
        Task<int> Edit(User entity, string ipaddress, int activeuser);
        Task<int> Delete(int Menuid, string ipaddress, int activeuser, string user = "");
        List<User> GetUser(int id);
        User GetSingleUser(int id);
        Task<User> IsUserValid(string userid, string upassword);

        Task<int> ChangePassword(string userid, string upassword, int isReset, string ipaddress, int activeuser);

    }



    public class UserService : IUserService
    {

        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public UserService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public async Task<int> Create(User entity, string ipaddress, int activeuser)
        {
            string query = "INSERT INTO ent_user (USER_CODE,[Password],USER_CREATEDDATE,USER_CREATEDBY,USER_ISDELETED,Role_ID,Company_ID,EssEnabled,PASSWORD_RESET) Values('" + entity.USER_CODE + "','" + entity.Password + "','" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm") + "','" + entity.ACTIVE_USER + "'," + "0 ," + entity.ROLE_ID + "," + entity.COMPANY_ID + ",'" + entity.EssEnabled + "','" + entity.PASSWORD_RESET + "')";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(User entity, string ipaddress, int activeuser)
        {
            string query = "Update ent_user set ROLE_ID = '" + entity.ROLE_ID + "' , Password ='" + CryptoHelper.EncryptTripleDES(entity.Password) + "'  where USER_ID =  " + entity.USER_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int UserId, string ipaddress, int activeuser, string user = "")
        {
            string query = "Update ent_user set user_isdeleted =1 , USER_DELETEDDATE ='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "', USER_DELETEDBY = '" + user + "' where USER_ID = " + UserId;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<User> GetUser(int id)
        {
            string query = string.Empty;
            User user = GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = "select  USER_ID,USER_CODE,Password, etu.ROLE_ID, ROLE_NAME , EssEnabled,PASSWORD_RESET,etu.COMPANY_ID , ec.COMPANY_NAME " +
                            " from ent_user etu , ent_role_master etr , ENT_COMPANY ec " +
                            " where etu.ROLE_ID = etr.ROLE_ID  and etu.COMPANY_ID = ec.COMPANY_ID and user_isdeleted =0 ";
            }
            else
            {
                query = "select  USER_ID,USER_CODE,Password, etu.ROLE_ID, ROLE_NAME , EssEnabled,PASSWORD_RESET,etu.COMPANY_ID , ec.COMPANY_NAME " +
                        " from ent_user etu , ent_role_master etr , ENT_COMPANY ec " +
                        " where etu.ROLE_ID = etr.ROLE_ID  and etu.COMPANY_ID = ec.COMPANY_ID and user_isdeleted =0 and ec.COMPANY_ID in(" +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }    
            

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<User> entUserList = new List<User>();

            foreach (DataRow dr in x.Rows)
            {
                User entUser = new User();
                entUser.USER_ID = Convert.ToInt16(dr["USER_ID"]);
                entUser.USER_CODE = Convert.ToString(dr["USER_CODE"]);
                entUser.ROLE_ID = Convert.ToInt16(dr["ROLE_ID"]);
                entUser.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                entUser.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entUser.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);

                entUserList.Add(entUser);
            }
            return entUserList;
        }

        public User GetSingleUser(int id)
        {
            string query = "select  USER_ID,USER_CODE,Password, etu.ROLE_ID, ROLE_NAME , EssEnabled,PASSWORD_RESET,etu.COMPANY_ID , ec.COMPANY_NAME " +
                           " from ent_user etu , ent_role_master etr , ENT_COMPANY ec " +
                           " where etu.ROLE_ID = etr.ROLE_ID  and etu.COMPANY_ID = ec.COMPANY_ID and user_isdeleted =0 and  USER_ID =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            User entUser = new User();

            foreach (DataRow dr in x.Rows)
            {
                //User entUser = new User();
                entUser.USER_ID = Convert.ToInt16(dr["USER_ID"]);
                entUser.USER_CODE = Convert.ToString(dr["USER_CODE"]);
                entUser.ROLE_ID = Convert.ToInt16(dr["ROLE_ID"]);
                entUser.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                entUser.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entUser.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                entUser.Password = Convert.ToString(dr["Password"]);
            }

            return entUser;
        }



        public async Task<User> IsUserValid(string userid, string upassword)
        {
            string query = "select  USER_ID,USER_CODE, etu.ROLE_ID, ROLE_NAME , EssEnabled,PASSWORD_RESET,etu.COMPANY_ID , etu.PASSWORD_RESET, ec.COMPANY_NAME " +
                         " from ent_user etu , ent_role_master etr , ENT_COMPANY ec " +
                         " where etu.ROLE_ID = etr.ROLE_ID  and etu.COMPANY_ID = ec.COMPANY_ID and user_isdeleted =0 and COMPANY_ISDELETED =0 and COMPANY_CODE+user_code ='" + userid + "' and Password ='" + upassword + "'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            User entUser = new User();

            foreach (DataRow dr in x.Rows)
            {
                //User entUser = new User();
                entUser.USER_ID = Convert.ToInt16(dr["USER_ID"]);
                entUser.USER_CODE = Convert.ToString(dr["USER_CODE"]);
                entUser.ROLE_ID = Convert.ToInt16(dr["ROLE_ID"]);
                entUser.ROLE_NAME = Convert.ToString(dr["ROLE_NAME"]);
                entUser.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                entUser.COMPANY_NAME = Convert.ToString(dr["COMPANY_NAME"]);
                //entUser.Password = Convert.ToString(dr["Password"]);
                entUser.EssEnabled = Convert.ToBoolean(dr["EssEnabled"]);
                entUser.PASSWORD_RESET = Convert.ToBoolean(dr["PASSWORD_RESET"]);
            }

            return entUser;
        }

        public async Task<int> ChangePassword(string userid, string upassword, int isReset, string ipaddress, int activeuser)
        {
            string query = "update ent_user set  [Password] ='" + upassword + "',PASSWORD_RESET=" + isReset + ",IS_SYNC=0 where USER_ID =" + userid;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}