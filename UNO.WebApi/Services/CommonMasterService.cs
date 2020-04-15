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
    public interface ICommonMasterService
    {
        List<CommonMaster> GetCommon();
        List<CommonMaster> GetCommonWithEmployee();
        Task<int> GetCommonIdByName(string name);
    }
    public class CommonMasterService : ICommonMasterService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public CommonMasterService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public List<CommonMaster> GetCommon()
        {
            string query = " select COMMON_TYPES_ID,COMMON_types as COMMON_NAME From ent_org_common_types where COMMON_TYPES not in('COMPANY','EMPLOYEE')  order by common_types ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<CommonMaster> entParamsList = new List<CommonMaster>();

            foreach (DataRow dr in x.Rows)
            {
                CommonMaster entity = new CommonMaster();
                entity.COMMON_TYPES_ID = Convert.ToInt32(dr["COMMON_TYPES_ID"]);
                entity.COMMON_NAME = Convert.ToString(dr["COMMON_NAME"]);
                entParamsList.Add(entity);
            }
            return entParamsList;
        }

        public async Task<int> GetCommonIdByName(string name)
        {
            string query = " select COMMON_TYPES_ID From ent_org_common_types where COMMON_TYPES = '" + name + "'  order by common_types ";
            return await _DatabaseHelper.GetScalarValue(query, CommandType.Text);
        }


        public List<CommonMaster> GetCommonWithEmployee()
        {
            string query = " select COMMON_TYPES_ID,COMMON_types as COMMON_NAME From ent_org_common_types where COMMON_TYPES not in('COMPANY')  order by common_types ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<CommonMaster> entParamsList = new List<CommonMaster>();

            foreach (DataRow dr in x.Rows)
            {
                CommonMaster entity = new CommonMaster();
                entity.COMMON_TYPES_ID = Convert.ToInt32(dr["COMMON_TYPES_ID"]);
                entity.COMMON_NAME = Convert.ToString(dr["COMMON_NAME"]);
                entParamsList.Add(entity);
            }
            return entParamsList;
        }
    }
}