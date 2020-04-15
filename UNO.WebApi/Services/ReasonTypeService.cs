using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IReasonTypeService
    {
        List<Reason_Type> Get();
    }
    public class ReasonTypeService : IReasonTypeService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public ReasonTypeService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<Reason_Type> Get()
        {

            string query = "SELECT REASON_TYPE_ID,REASON_DESC FROM ent_reason_type "; //Put company code in where condition

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Reason_Type> entModuleList = new List<Reason_Type>();

            foreach (DataRow dr in x.Rows)
            {
                Reason_Type entModule = new Reason_Type();
                entModule.REASON_TYPE_ID = Convert.ToInt32(dr["REASON_TYPE_ID"]);
                entModule.REASON_DESC = Convert.ToString(dr["REASON_DESC"]);
                entModuleList.Add(entModule);
            }
            return entModuleList;
        }
    }
}