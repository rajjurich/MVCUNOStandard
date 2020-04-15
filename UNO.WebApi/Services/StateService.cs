using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IStateService
    {
        List<State> Get();
    }
    public class StateService :IStateService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public StateService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<State> Get()
        {

            string query = "SELECT CODE,VALUE FROM ent_params WHERE IDENTIFIER= 'STATES' and MODULE='STATES'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<State> entModuleList = new List<State>();

            foreach (DataRow dr in x.Rows)
            {
                State entModule = new State();
                entModule.STATE_CODE = Convert.ToString(dr["CODE"]);
                entModule.STATE_NAME = Convert.ToString(dr["VALUE"]);
                entModuleList.Add(entModule);
            }
            return entModuleList;
        }
    }
}