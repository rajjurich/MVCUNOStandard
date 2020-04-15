using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IShiftPatternService
    {
        Task<int> Create(ShiftPattern entity, string ipaddress, int activeuser);
        Task<int> Edit(ShiftPattern entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        IQueryable<ShiftPatternDto> Get(int id);
        Task<ShiftPattern> GetSingle(int id);

        IQueryable<ShiftPattern> getshiftpattern(int id);
    }
    public class ShiftPatternService : IShiftPatternService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public ShiftPatternService(UnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task<int> Create(ShiftPattern entity, string ipaddress, int activeuser)
        {
            string query = " Insert into TA_SHIFT_PATTERN (SHIFT_PATTERN_CODE,SHIFT_PATTERN_DESCRIPTION,SHIFT_PATTERN_TYPE,SHIFT_PATTERN,SHIFT_ISDELETED,COMPANY_ID) " +
                         " Values ('" + entity.SHIFT_PATTERN_CODE + "','" + entity.SHIFT_PATTERN_DESCRIPTION + "','" + entity.SHIFT_PATTERN_TYPE + "','" + entity.SHIFT_PATTERN + "',0,'" + entity.COMPANY_ID + "') ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(ShiftPattern entity, string ipaddress, int activeuser)
        {
            string query = " update  TA_SHIFT_PATTERN set SHIFT_PATTERN_CODE='" + entity.SHIFT_PATTERN_CODE + "',SHIFT_PATTERN_DESCRIPTION='" + entity.SHIFT_PATTERN_DESCRIPTION + "' " +
                           " ,SHIFT_PATTERN_TYPE='" + entity.SHIFT_PATTERN_TYPE + "', SHIFT_PATTERN='" + entity.SHIFT_PATTERN + "',COMPANY_ID='" + entity.COMPANY_ID + "' where SHIFT_PATTERN_ID = '" + entity.SHIFT_PATTERN_ID + "'";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " update  TA_SHIFT_PATTERN set SHIFT_ISDELETED=1 " +
                           " ,SHIFT_DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "' where SHIFT_PATTERN_ID = '" + id + "'";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public IQueryable<ShiftPatternDto> Get(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select distinct TA_SHIFT_PATTERN.SHIFT_PATTERN_ID,SHIFT_PATTERN_CODE,SHIFT_PATTERN_DESCRIPTION,VALUE as SHIFT_PATTERN_TYPE from shift_shift_pattern join ta_shift on ta_shift.SHIFT_ID = shift_shift_pattern.SHIFT_ID join TA_SHIFT_PATTERN on TA_SHIFT_PATTERN.SHIFT_PATTERN_ID = shift_shift_pattern.SHIFT_PATTERN_ID join ent_params on ent_params.CODE = TA_SHIFT_PATTERN.SHIFT_PATTERN_TYPE where TA_SHIFT_PATTERN.SHIFT_ISDELETED =0 ";
            }
            else
            {
                query = " select distinct TA_SHIFT_PATTERN.SHIFT_PATTERN_ID,SHIFT_PATTERN_CODE,SHIFT_PATTERN_DESCRIPTION,VALUE as SHIFT_PATTERN_TYPE from shift_shift_pattern join ta_shift on ta_shift.SHIFT_ID = shift_shift_pattern.SHIFT_ID join TA_SHIFT_PATTERN on TA_SHIFT_PATTERN.SHIFT_PATTERN_ID = shift_shift_pattern.SHIFT_PATTERN_ID join ent_params on ent_params.CODE = TA_SHIFT_PATTERN.SHIFT_PATTERN_TYPE where TA_SHIFT_PATTERN.SHIFT_ISDELETED =0 and TA_SHIFT_PATTERN.COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ShiftPatternDto> shiftPatternList = new List<ShiftPatternDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    ShiftPatternDto shiftPattern = new ShiftPatternDto();
                    shiftPattern.SHIFT_PATTERN_ID = Convert.ToInt32(dr["SHIFT_PATTERN_ID"]);
                    shiftPattern.SHIFT_PATTERN_CODE = Convert.ToString(dr["SHIFT_PATTERN_CODE"]);
                    shiftPattern.SHIFT_PATTERN_DESCRIPTION = Convert.ToString(dr["SHIFT_PATTERN_DESCRIPTION"]);
                    shiftPattern.SHIFT_PATTERN_TYPE = Convert.ToString(dr["SHIFT_PATTERN_TYPE"]);
                    shiftPatternList.Add(shiftPattern);
                }
            }

            return shiftPatternList.AsQueryable();
        }

        public Task<ShiftPattern> GetSingle(int id)
        {
            string query = " select * from TA_SHIFT_PATTERN where SHIFT_PATTERN_ID= " + id;



            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForShiftPattern = dataSet.Tables[0];

            ShiftPattern shiftPattern = new ShiftPattern();

            if (dataTableForShiftPattern.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForShiftPattern.Rows)
                {

                    shiftPattern.SHIFT_PATTERN_ID = Convert.ToInt32(dr["SHIFT_PATTERN_ID"]);
                    shiftPattern.SHIFT_PATTERN_CODE = Convert.ToString(dr["SHIFT_PATTERN_CODE"]);
                    shiftPattern.SHIFT_PATTERN_DESCRIPTION = Convert.ToString(dr["SHIFT_PATTERN_DESCRIPTION"]);
                    shiftPattern.SHIFT_PATTERN_TYPE = Convert.ToString(dr["SHIFT_PATTERN_TYPE"]);
                    shiftPattern.SHIFT_PATTERN = Convert.ToString(dr["SHIFT_PATTERN"]);
                    shiftPattern.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);

                }
            }


            return Task.Run(() =>
            {
                return shiftPattern;
            });
        }

        public IQueryable<ShiftPattern> getshiftpattern(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = "SELECT SHIFT_PATTERN_ID, SHIFT_PATTERN_CODE + ' - ' + SHIFT_PATTERN_DESCRIPTION AS SHIFT_PATTERN_DESCRIPTION FROM TA_SHIFT_PATTERN WHERE SHIFT_ISDELETED = '0' ";
            }
            else
            {
                query = "SELECT SHIFT_PATTERN_ID, SHIFT_PATTERN_CODE + ' - ' + SHIFT_PATTERN_DESCRIPTION AS SHIFT_PATTERN_DESCRIPTION FROM TA_SHIFT_PATTERN WHERE SHIFT_ISDELETED = '0'  and COMPANY_ID in(" + CustomQueryies.Queries.RoleDataAccessQuery(id)+") ";
            }
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);

            List<ShiftPattern> shiftpatternlist = new List<ShiftPattern>();

            foreach (DataRow dr in x.Rows)
            {
                ShiftPattern shiftpatterndto = new ShiftPattern();
                shiftpatterndto.SHIFT_PATTERN_ID = Convert.ToInt32(dr["SHIFT_PATTERN_ID"]);
                shiftpatterndto.SHIFT_PATTERN_DESCRIPTION = Convert.ToString(dr["SHIFT_PATTERN_DESCRIPTION"]);
                shiftpatternlist.Add(shiftpatterndto);
            }
            return shiftpatternlist.AsQueryable();
        }
    }
}