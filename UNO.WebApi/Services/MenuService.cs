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
    public interface IMenuService
    {
        Task<int> Create(Menu entity, string ipaddress, int activeuser);
        Task<int> Edit(Menu entity, string ipaddress, int activeuser);
        Task<int> Delete(int Menuid, string ipaddress, int activeuser);
        List<Menu> GetMenu();
        Menu GetMenu(int id);
        Task<bool> IsUniqMenuUrl(Menu entity, bool isEdit);
    }


    public class MenuService : IMenuService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public MenuService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task<int> Create(Menu entity, string ipaddress, int activeuser)
        {
            string query = " insert into ent_menu_master(MENU_NAME,MENU_URL,MODULE_ID,SMODULE_ID,MENU_ITEMPOSITION,MENU_IsDeleted)  values ('" + entity.MENU_NAME + "', '" + entity.MENU_URL + "', " + entity.MODULE_ID + ", " + entity.SMODULE_ID + "," + entity.MENU_ITEMPOSITION + ",0) ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }
        public async Task<int> Edit(Menu entity, string ipaddress, int activeuser)
        {
            string query = "Update ent_menu_master set MENU_NAME = '" + entity.MENU_NAME + "', MENU_URL = '" + entity.MENU_URL + "' , MODULE_ID = " + entity.MODULE_ID + " , SMODULE_ID =" + entity.SMODULE_ID + " , MENU_ITEMPOSITION="+ entity.MENU_ITEMPOSITION + "  where MENU_ID =  " + entity.MENU_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int Menuid, string ipaddress, int activeuser)
        {
            string query = "Update ent_menu_master set MENU_IsDeleted =1 , MENU_DeletedDate ='" + DateTime.Now + "' where MENU_ID = " + Menuid;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<Menu> GetMenu()
        {
            string query = "select MENU_ID,MENU_NAME,MENU_URL, mnu.MODULE_ID, MODULE_NAME , mnu.SMODULE_ID, SMODULE_NAME , MENU_ITEMPOSITION from ent_menu_master mnu , ent_module_master modMas,ent_sub_module_master submod where modMas.MODULE_ID = mnu.MODULE_ID and mnu.SMODULE_ID = submod.SMODULE_ID and MENU_IsDeleted =0";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Menu> entParamsList = new List<Menu>();

            foreach (DataRow dr in x.Rows)
            {
                Menu entMenu = new Menu();
                entMenu.MENU_ID = Convert.ToInt32(dr["MENU_ID"]);
                entMenu.MENU_NAME = Convert.ToString(dr["MENU_NAME"]);
                entMenu.MENU_URL = Convert.ToString(dr["MENU_URL"]);
                entMenu.MODULE_NAME = Convert.ToString(dr["MODULE_NAME"]);
                entMenu.MODULE_ID = Convert.ToInt32(dr["MODULE_ID"]);
                entMenu.SMODULE_NAME = Convert.ToString(dr["SMODULE_NAME"]);
                entMenu.SMODULE_ID = Convert.ToInt32(dr["SMODULE_ID"]);
                entMenu.MENU_ITEMPOSITION = Convert.ToInt32(dr["MENU_ITEMPOSITION"]);
                entParamsList.Add(entMenu);
            }
            return entParamsList;

        }




        public Menu GetMenu(int id)
         {
            string query = "select MENU_ID,MENU_NAME,MENU_URL, mnu.MODULE_ID, MODULE_NAME , mnu.SMODULE_ID, SMODULE_NAME ,MENU_ITEMPOSITION from ent_menu_master mnu , ent_module_master modMas,ent_sub_module_master submod where modMas.MODULE_ID = mnu.MODULE_ID and mnu.SMODULE_ID = submod.SMODULE_ID and MENU_IsDeleted =0 and menu_id =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            Menu entParamsList = new Menu();

            foreach (DataRow dr in x.Rows)
            {
                entParamsList.MENU_ID = Convert.ToInt32(dr["MENU_ID"]);
                entParamsList.MENU_NAME = Convert.ToString(dr["MENU_NAME"]);
                entParamsList.MENU_URL = Convert.ToString(dr["MENU_URL"]);
                entParamsList.MODULE_NAME = Convert.ToString(dr["MODULE_NAME"]);
                entParamsList.MODULE_ID = Convert.ToInt32(dr["MODULE_ID"]);
                entParamsList.SMODULE_NAME = Convert.ToString(dr["SMODULE_NAME"]);
                entParamsList.SMODULE_ID = Convert.ToInt32(dr["SMODULE_ID"]);
                entParamsList.MENU_ITEMPOSITION = Convert.ToInt32(dr["MENU_ITEMPOSITION"]);
            }
            return entParamsList;


        }
        public async Task<bool> IsUniqMenuUrl(Menu entity, bool isEdit)
        {
            string query = "SELECT count(MENU_URL) FROM ent_menu_master WHERE MENU_URL = '" + entity.MENU_URL + "' and MENU_IsDeleted =0";
            if (isEdit)
                query = query + " and MENU_ID<>" + entity.MENU_ID;

            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text) == 0 ? false : true;
            return x;

        }
    }
}