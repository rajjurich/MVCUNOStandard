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
    public interface ICommonTypesService
    {
        List<CommonTypesModel> Get(int id);
    }

    public class CommonTypesService : ICommonTypesService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public CommonTypesService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<CommonTypesModel> Get(int id)
        {
            string query = "select eog.* from ENT_ORG_COMMON_TYPES eog "
                           + " order by eog.ORG_TYPES_ORDER asc";


            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            List<CommonTypesModel> companylist = new List<CommonTypesModel>();

            foreach (DataRow dr in x.Rows)
            {
                CommonTypesModel c = new CommonTypesModel();
                c.CommonTypes = Convert.ToString(dr["COMMON_TYPES"]);
                companylist.Add(c);
            }

            return companylist;
        }
    }
}
