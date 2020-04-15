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
    public interface IEmployeeAccesssService
    {
        Task<EmployeeAccess> GetSingle(int id);
    }
    public class EmployeeAccesssService : IEmployeeAccesssService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public EmployeeAccesssService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public Task<EmployeeAccess> GetSingle(int id)
        {
            string query = "select distinct ENTITY_TYPE from eal_config where ISDELETED=0 and ENTITY_ID in (select ENTITY_ID from eal_config where eal_id=" + id + ")";

            query += " ; select distinct ENTITY_ID from eal_config where ISDELETED=0 and ENTITY_ID in (select ENTITY_ID from eal_config where eal_id=" + id + ")";
            query += " ; select distinct AL_ID from eal_config where ISDELETED=0 and ENTITY_ID in (select ENTITY_ID from eal_config where eal_id=" + id + ")";
            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);
            var dataTableForEntityType = dataSet.Tables[0];
            var dataTableForEntityId = dataSet.Tables[1];
            var dataTableForAlId = dataSet.Tables[2];
            EmployeeAccess employeeAccess = new EmployeeAccess();

            List<int> entityIds = new List<int>();
            List<long> aL_IDs = new List<long>();
            if (dataTableForEntityType.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForEntityType.Rows)
                {
                    employeeAccess.COMMON_TYPES_ID = Convert.ToInt32(dr["ENTITY_TYPE"]);
                   
                }
            }

            if (dataTableForEntityId.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForEntityId.Rows)
                {
                    entityIds.Add(Convert.ToInt32(dr["ENTITY_ID"]));

                }
            }

            if (dataTableForAlId.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForAlId.Rows)
                {
                    aL_IDs.Add(Convert.ToInt32(dr["AL_ID"]));

                }
            }


            employeeAccess.EntityIds = entityIds;
            employeeAccess.AL_IDs = aL_IDs;

            return Task.Run(() =>
            {
                return employeeAccess;
            });

        }
    }
}