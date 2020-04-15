using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface ICompanyTypeService
    {
        List<CompanyType> Get();
    }
    public class CompanyTypeService : ICompanyTypeService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public CompanyTypeService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<CompanyType> Get()
        {

            string query = "select ADDRESS_TYPE_ID,ADDRESS_TYPE from  ENT_COMPANY_ADDRESS_TYPE";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<CompanyType> entModuleList = new List<CompanyType>();

            foreach (DataRow dr in x.Rows)
            {
                CompanyType entModule = new CompanyType();
                entModule.ADDRESS_TYPE_ID = Convert.ToInt16(dr["ADDRESS_TYPE_ID"]);
                entModule.ADDRESS_TYPE = Convert.ToString(dr["ADDRESS_TYPE"]);

                entModuleList.Add(entModule);
            }
            return entModuleList;
        }
    }
}