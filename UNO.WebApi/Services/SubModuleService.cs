using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface ISubModuleService
    {
        List<SubModule> Get();
        List<SubModule> Get(string id);
    }
    public class SubModuleService : ISubModuleService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public SubModuleService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }


        public List<SubModule> Get()
        {

            string query = "select SMODULE_ID,SMODULE_NAME from  ent_sub_module_master";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<SubModule> entsubModuleList = new List<SubModule>();

            foreach (DataRow dr in x.Rows)
            {
                SubModule entModule = new SubModule();
                entModule.SMODULE_ID = Convert.ToInt16(dr["SMODULE_ID"]);
                entModule.SMODULE_NAME = Convert.ToString(dr["SMODULE_NAME"]);

                entsubModuleList.Add(entModule);
            }
            return entsubModuleList;
        }

        public List<SubModule> Get(string id)
        {

            string query = "select SMODULE_ID,SMODULE_NAME from  ent_sub_module_master where MODULE_ID =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<SubModule> entsubModuleList = new List<SubModule>();

            foreach (DataRow dr in x.Rows)
            {
                SubModule entModule = new SubModule();
                entModule.SMODULE_ID = Convert.ToInt16(dr["SMODULE_ID"]);
                entModule.SMODULE_NAME = Convert.ToString(dr["SMODULE_NAME"]);

                entsubModuleList.Add(entModule);
            }
            return entsubModuleList;
        }



    }
}