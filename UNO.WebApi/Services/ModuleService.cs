using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IModuleService
    {
        List<Module> Get();

        List<Module> GetUserModule(int UserId);
    }
    public class ModuleService : IModuleService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public ModuleService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }


        public List<Module> Get()
        {

            string query = "select module_id,Module_name from  ent_module_master";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Module> entModuleList = new List<Module>();

            foreach (DataRow dr in x.Rows)
            {
                Module entModule = new Module();
                entModule.MODULE_ID = Convert.ToInt16(dr["MODULE_ID"]);
                entModule.MODULE_NAME = Convert.ToString(dr["MODULE_NAME"]);

                entModuleList.Add(entModule);
            }
            return entModuleList;
        }





        public List<Module> GetUserModule(int UserId)
        {

            string query = "select module_id,Module_name from  ent_module_master";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Module> entModuleList = new List<Module>();

            foreach (DataRow dr in x.Rows)
            {
                Module entModule = new Module();
                entModule.MODULE_ID = Convert.ToInt16(dr["MODULE_ID"]);
                entModule.MODULE_NAME = Convert.ToString(dr["MODULE_NAME"]);

                entModuleList.Add(entModule);
            }
            return entModuleList;

        }
    }
}