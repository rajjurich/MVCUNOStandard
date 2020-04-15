using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IEntParamsService
    {
        List<EntParams> Get(string identifier);
    }
    public class EntParamsService : IEntParamsService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public EntParamsService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<EntParams> Get(string identifier)
        {
            string query = "SELECT PARAM_ID,VALUE,CODE FROM ENT_PARAMS WHERE IDENTIFIER = '" + identifier + "'";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EntParams> entParams = new List<EntParams>();

            foreach (DataRow dr in x.Rows)
            {
                EntParams entParam = new EntParams();
                entParam.PARAM_ID = Convert.ToInt32(dr["PARAM_ID"]);
                entParam.VALUE = Convert.ToString(dr["VALUE"]);
                entParam.CODE = Convert.ToString(dr["CODE"]);
                entParams.Add(entParam);
            }
            return entParams;
        }
    }
}