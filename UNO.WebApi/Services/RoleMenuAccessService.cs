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
    public interface IRoleMenuAccessService
    {
        RoleMenuAccess GetRoleMenuAccess(string Link, string RoleId);
        Task<List<RoleMenuAccess>> GetAllMenuRoleWise(string RoleId);
        List<MenuAccessRoleWise> GetMenuRoleWise(string RoleId);
        Task<int> InsertRoleMenuAccessBulk(List<RoleMenuAccess> objRMAList, string ipaddress, int activeuser);
        Task<int> InsertRoleMenuAccess(RoleMenuAccess objRMA, string ipaddress, int activeuser);
    }


    public class RoleMenuAccessService : IRoleMenuAccessService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public RoleMenuAccessService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public RoleMenuAccess GetRoleMenuAccess(string Link, string RoleId)
        {
            // Logic :- Get Single Menu access(add, edit, delete and view) Assigne to role
            string query = "select ROLE_ACCESS_ID, rma.ROLE_ID , rma.MENU_ID ,ROLE_ADD , ROLE_VIEW ,ROLE_EDIT ,ROLE_DELETE, ROLE_IsDeleted from ent_role_menu_access  rma , ent_menu_master mm where rma.menu_id = mm.menu_id and rma.ROLE_ID = " + RoleId + " and menu_url ='" + Link + "'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            RoleMenuAccess entMenuAccess = new RoleMenuAccess();

            foreach (DataRow dr in x.Rows)
            {
                //RoleMenuAccess entMenu = new RoleMenuAccess();
                entMenuAccess.ROLE_ACCESS_ID = DataConversion.Int64DataType(dr["ROLE_ACCESS_ID"]);
                entMenuAccess.ROLE_ID = Convert.ToString(dr["ROLE_ID"]);
                entMenuAccess.MENU_ID = Convert.ToString(dr["MENU_ID"]);
                entMenuAccess.CreateAccess = Convert.ToInt16(dr["ROLE_ADD"]);
                entMenuAccess.ViewAccess = Convert.ToInt16(dr["ROLE_VIEW"]);
                entMenuAccess.UpdateAccess = Convert.ToInt16(dr["ROLE_EDIT"]);
                entMenuAccess.DeleteAccess = Convert.ToInt16(dr["ROLE_DELETE"]);
                entMenuAccess.IsDeleted = Convert.ToInt16(dr["ROLE_IsDeleted"]);
                //entModuleList.Add(entMenu);
            }
            return entMenuAccess;
        }

        public Task<List<RoleMenuAccess>> GetAllMenuRoleWise(string RoleId)
        {
            string query = "select rma.ROLE_ACCESS_ID, rma.ROLE_ID ,mm.Menu_Name, mm.menu_id ,rma.ROLE_ADD , rma.ROLE_VIEW ,rma.ROLE_EDIT ,rma.ROLE_DELETE, rma.ROLE_IsDeleted from ENT_ROLE_MENU_ACCESS rma right join   ent_menu_master mm on rma.menu_id = mm.menu_id and rma.ROLE_ID = " + RoleId + " where mm.menu_id in (select menu_Id from MenuCompanyRelation where company_id = (select top 1 company_id from ENT_ROLE_MASTER where role_id =" + RoleId + "))   and mm.MENU_IsDeleted=0 order by role_access_id desc ";

            DataTable x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<RoleMenuAccess> entModuleList = new List<RoleMenuAccess>();

            foreach (DataRow dr in x.Rows)
            {
                RoleMenuAccess entMenuAccess = new RoleMenuAccess();
                entMenuAccess.ROLE_ACCESS_ID = DataConversion.Int32DataType(dr["ROLE_ACCESS_ID"]);
                entMenuAccess.ROLE_ID = DataConversion.StringDataType(dr["ROLE_ID"]);
                entMenuAccess.MENU_ID = DataConversion.StringDataType(dr["MENU_ID"]);
                entMenuAccess.MENU_NAME = DataConversion.StringDataType(dr["Menu_Name"]);
                entMenuAccess.CreateAccess = DataConversion.ShortDataType(dr["ROLE_ADD"]);
                entMenuAccess.ViewAccess = DataConversion.ShortDataType(dr["ROLE_VIEW"]);
                entMenuAccess.UpdateAccess = DataConversion.ShortDataType(dr["ROLE_EDIT"]);
                entMenuAccess.DeleteAccess = DataConversion.ShortDataType(dr["ROLE_DELETE"]);
                entMenuAccess.IsDeleted = DataConversion.ShortDataType(dr["ROLE_IsDeleted"]);
                entModuleList.Add(entMenuAccess);
            }

            return Task.Run(() =>
            {
                return entModuleList;
            });
            //return entModuleList;
        }

        //public async Task<int> InsertRoleMenuAccessBulk(List<RoleMenuAccess> objRMA)
        //{
        //    RoleMenuAccess mstobjrm = new RoleMenuAccess();
        //    string query = string.Empty;
        //    foreach (RoleMenuAccess entity in objRMA)
        //    {
        //        RoleMenuAccess objromemenuaccess = new RoleMenuAccess();
        //        query += " Update ENT_ROLE_MENU_ACCESS set MENU_ID = '" + entity.MENU_ID + "', ROLE_ID = '" + entity.ROLE_ID + "' , ROLE_VIEW = " + entity.ViewAccess + " , ROLE_ADD =" + entity.CreateAccess + " , ROLE_EDIT=" + entity.UpdateAccess + " , ROLE_DELETE=" + entity.DeleteAccess + "  where ROLE_ACCESS_ID =  " + entity.ROLE_ACCESS_ID;
        //    }
        //    //return await _DatabaseHelper.GetScalarValue(query, CommandType.Text, null);
        //    return await _DatabaseHelper.Insert(query, CommandType.Text);

        //}

        public async Task<int> InsertRoleMenuAccessBulk(List<RoleMenuAccess> objRMA, string ipaddress, int activeuser)
        {
            RoleMenuAccess mstobjrm = new RoleMenuAccess();
            string query = string.Empty;
            int x = 0;
            foreach (RoleMenuAccess entity in objRMA)
            {

                RoleMenuAccess objromemenuaccess = new RoleMenuAccess();
                //query = " Update ENT_ROLE_MENU_ACCESS set MENU_ID = '" + entity.MENU_ID + "', ROLE_ID = '" + entity.ROLE_ID + "' , ROLE_VIEW = " + entity.ViewAccess + " , ROLE_ADD =" + entity.CreateAccess + " , ROLE_EDIT=" + entity.UpdateAccess + " , ROLE_DELETE=" + entity.DeleteAccess + "  where ROLE_ACCESS_ID =  " + entity.ROLE_ACCESS_ID;
                //return await _DatabaseHelper.Insert(query, CommandType.Text);


                if (entity.ROLE_ACCESS_ID == 0 && (entity.ViewAccess == 1 || entity.UpdateAccess == 1 || entity.CreateAccess == 1 || entity.DeleteAccess == 1))
                {
                    query += " insert into ENT_ROLE_MENU_ACCESS(ROLE_ADD,ROLE_DELETE,ROLE_EDIT,ROLE_VIEW,ROLE_IsDeleted,IS_SYNC,ROLE_ID,MENU_ID) values ('" + entity.CreateAccess + "','" + entity.DeleteAccess + "','" + entity.UpdateAccess + "','" + entity.ViewAccess + "',0,0,'" + entity.ROLE_ID + "','" + entity.MENU_ID + "');";
                }
                else if (entity.ROLE_ACCESS_ID > 0)
                {
                    query += " Update ENT_ROLE_MENU_ACCESS set MENU_ID = '" + entity.MENU_ID + "', ROLE_ID = '" + entity.ROLE_ID + "' , ROLE_VIEW = " + entity.ViewAccess + " , ROLE_ADD =" + entity.CreateAccess + " , ROLE_EDIT=" + entity.UpdateAccess + " , ROLE_DELETE=" + entity.DeleteAccess + "  where ROLE_ACCESS_ID =  " + entity.ROLE_ACCESS_ID + ";";
                }

            }
            x = await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
            //return await _DatabaseHelper.GetScalarValue(query, CommandType.Text, null);
            return x;// await _DatabaseHelper.Insert(query, CommandType.Text);


        }

        public async Task<int> InsertRoleMenuAccess(RoleMenuAccess entity, string ipaddress, int activeuser)
        {
            string query = string.Empty;
            if (entity.ROLE_ACCESS_ID == 0)
            {
                query += " insert into ENT_ROLE_MENU_ACCESS(ROLE_ADD,ROLE_DELETE,ROLE_EDIT,ROLE_VIEW,ROLE_IsDeleted,IS_SYNC,ROLE_ID,MENU_ID) values ('" + entity.CreateAccess + "','" + entity.DeleteAccess + "','" + entity.UpdateAccess + "','" + entity.ViewAccess + "',0,0,'" + entity.ROLE_ID + "','" + entity.MENU_ID + "');";
            }
            else
            {
                query += " Update ENT_ROLE_MENU_ACCESS set MENU_ID = '" + entity.MENU_ID + "', ROLE_ID = '" + entity.ROLE_ID + "' , ROLE_VIEW = " + entity.ViewAccess + " , ROLE_ADD =" + entity.CreateAccess + " , ROLE_EDIT=" + entity.UpdateAccess + " , ROLE_DELETE=" + entity.DeleteAccess + "  where ROLE_ACCESS_ID =  " + entity.ROLE_ACCESS_ID + ";";
            }

            int x = 0;
            x = await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser );
            return x;
            //RoleMenuAccess mstobjrm = new RoleMenuAccess();
            //string query = string.Empty;
            //int x = 0;
            //foreach (RoleMenuAccess entity in objRMA)
            //{

            //    RoleMenuAccess objromemenuaccess = new RoleMenuAccess();
            //    //query = " Update ENT_ROLE_MENU_ACCESS set MENU_ID = '" + entity.MENU_ID + "', ROLE_ID = '" + entity.ROLE_ID + "' , ROLE_VIEW = " + entity.ViewAccess + " , ROLE_ADD =" + entity.CreateAccess + " , ROLE_EDIT=" + entity.UpdateAccess + " , ROLE_DELETE=" + entity.DeleteAccess + "  where ROLE_ACCESS_ID =  " + entity.ROLE_ACCESS_ID;
            //    //return await _DatabaseHelper.Insert(query, CommandType.Text);


            //    if (entity.ROLE_ACCESS_ID == 0)
            //    {
            //        query += " insert into ENT_ROLE_MENU_ACCESS(ROLE_ADD,ROLE_DELETE,ROLE_EDIT,ROLE_VIEW,ROLE_IsDeleted,IS_SYNC,ROLE_ID,MENU_ID) values ('" + entity.CreateAccess + "','" + entity.DeleteAccess + "','" + entity.UpdateAccess + "','" + entity.ViewAccess + "',0,0,'" + entity.ROLE_ID + "','" + entity.MENU_ID + "');";
            //    }
            //    else
            //    {
            //        query += " Update ENT_ROLE_MENU_ACCESS set MENU_ID = '" + entity.MENU_ID + "', ROLE_ID = '" + entity.ROLE_ID + "' , ROLE_VIEW = " + entity.ViewAccess + " , ROLE_ADD =" + entity.CreateAccess + " , ROLE_EDIT=" + entity.UpdateAccess + " , ROLE_DELETE=" + entity.DeleteAccess + "  where ROLE_ACCESS_ID =  " + entity.ROLE_ACCESS_ID + ";";
            //    }

            //}
            //x = await _DatabaseHelper.Insert(query, CommandType.Text);
            ////return await _DatabaseHelper.GetScalarValue(query, CommandType.Text, null);
            //return x;// await _DatabaseHelper.Insert(query, CommandType.Text);


        }

        public List<MenuAccessRoleWise> GetMenuRoleWise(string RoleId)
        {
            // Logic :-  Menu's matching to roles will get selected.

            string query = "select enmm.MODULE_ID,enmm.MODULE_NAME,mm.MENU_ID,MENU_NAME,mm.MENU_URL,mm.MENU_ITEMPOSITION,snsbmm.SMODULE_ID,snsbmm.SMODULE_NAME from ent_role_menu_access  rma , ent_menu_master mm ,ent_module_master enmm,ENT_SUB_MODULE_MASTER snsbmm where rma.menu_id = mm.menu_id and mm.MODULE_ID = enmm.MODULE_ID and enmm.MODULE_ID = snsbmm.MODULE_ID  and snsbmm.SMODULE_ID = mm.SMODULE_ID and MENU_IsDeleted = 0 and ROLE_IsDeleted =0 and rma.ROLE_ID= " + RoleId + " and (rma.role_add=1 or rma.role_delete=1 or rma.role_edit=1 or rma.role_view=1) order by MODULE_ID, snsbmm.SMODULE_ID,mm.MENU_ITEMPOSITION";

            DataTable x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<MenuAccessRoleWise> entMenuList = new List<MenuAccessRoleWise>();

            foreach (DataRow dr in x.Rows)
            {
                MenuAccessRoleWise entMenuAccess = new MenuAccessRoleWise();
                entMenuAccess.MODULE_ID = Convert.ToInt32(dr["MODULE_ID"]);
                entMenuAccess.MODULE_NAME = Convert.ToString(dr["MODULE_NAME"]);
                entMenuAccess.MENU_ID = Convert.ToInt32(dr["MENU_ID"]);
                entMenuAccess.MENU_NAME = Convert.ToString(dr["MENU_NAME"]);
                entMenuAccess.MENU_URL = Convert.ToString(dr["MENU_URL"]);
                entMenuAccess.MENU_ITEMPOSITION = Convert.ToDouble(dr["MENU_ITEMPOSITION"]);
                entMenuAccess.SMODULE_ID = Convert.ToInt32(dr["SMODULE_ID"]);
                entMenuAccess.SMODULE_NAME = Convert.ToString(dr["SMODULE_NAME"]);
                entMenuList.Add(entMenuAccess);
            }
            return entMenuList;
        }
    }
}