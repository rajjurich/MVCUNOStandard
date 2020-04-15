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
    public interface IShiftService
    {
        Task<int> Create(Shift entity, string ipaddress, int activeuser);
        Task<int> Edit(Shift entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        IQueryable<ShiftDto> Get(int id);
        Task<Shift> GetSingle(int id);
    }
    public class ShiftService : IShiftService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public ShiftService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task<int> Create(Shift entity, string ipaddress, int activeuser)
        {

            string query = " Insert into ta_shift (SHIFT_CODE,SHIFT_DESCRIPTION,SHIFT_ALLOCATION_TYPE,SHIFT_AUTO_SEARCH_START,SHIFT_AUTO_SEARCH_END," +
                          " SHIFT_TYPE,SHIFT_START,SHIFT_END,SHIFT_BREAK_START, " +
                          " SHIFT_BREAK_END,SHIFT_BREAK_HRS,SHIFT_WORKHRS,SHIFT_HALFDAYWORKHRS, " +
                          " SHIFT_FLAG_ADD_BREAK,SHIFT_WEEKEND_DIFF_TIME,SHIFT_WEEKEND_START,SHIFT_WEEKEND_END, " +
                          " SHIFT_WEEKEND_BREAK_START,SHIFT_WEEKEND_BREAK_END,SHIFT_EARLY_SEARCH_HRS,SHIFT_LATE_SEARCH_HRS, " +
                          " SHIFT_CREATEDDATE,SHIFT_CREATEDBY,SHIFT_ISDELETED,COMPANY_ID) " +
                          " Values ('" + entity.SHIFT_CODE + "','" + entity.SHIFT_DESCRIPTION + "','" + entity.SHIFT_ALLOCATION_TYPE + "', " +
                          " '" + entity.SHIFT_AUTO_SEARCH_START + "' ,'" + entity.SHIFT_AUTO_SEARCH_END + "','" + entity.SHIFT_TYPE + "'," +
                          " '" + entity.SHIFT_START + "' , '" + entity.SHIFT_END + "', '" + entity.SHIFT_BREAK_START + "', " +
                          " '" + entity.SHIFT_BREAK_END + "' , '" + entity.SHIFT_BREAK_HRS + "', '" + entity.SHIFT_WORKHRS + "', '" + entity.SHIFT_HALFDAYWORKHRS + "', " +
                          " '" + entity.SHIFT_FLAG_ADD_BREAK + "' , '" + entity.SHIFT_WEEKEND_DIFF_TIME + "', '" + entity.SHIFT_WEEKEND_START + "', '" + entity.SHIFT_WEEKEND_END + "', " +
                          " '" + entity.SHIFT_WEEKEND_BREAK_START + "' , '" + entity.SHIFT_WEEKEND_BREAK_END + "', '" + entity.SHIFT_EARLY_SEARCH_HRS + "', '" + entity.SHIFT_LATE_SEARCH_HRS + "', " +
                          " '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "','" + entity.SHIFT_CREATEDBY + "',0,'" + entity.COMPANY_ID + "') ;";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(Shift entity, string ipaddress, int activeuser)
        {
            string query = " update ta_shift set SHIFT_CODE = '" + entity.SHIFT_CODE + "'" +
                           " ,SHIFT_DESCRIPTION = '" + entity.SHIFT_DESCRIPTION + "' " +
                           " ,SHIFT_ALLOCATION_TYPE = '" + entity.SHIFT_ALLOCATION_TYPE + "' " +
                           " ,SHIFT_AUTO_SEARCH_START = '" + entity.SHIFT_AUTO_SEARCH_START + "' " +
                           " ,SHIFT_AUTO_SEARCH_END = '" + entity.SHIFT_AUTO_SEARCH_END + "' " +
                           " ,SHIFT_TYPE = '" + entity.SHIFT_TYPE + "' " +
                           " ,SHIFT_START = '" + entity.SHIFT_START + "' " +
                           " ,SHIFT_END = '" + entity.SHIFT_END + "' " +
                           " ,SHIFT_BREAK_START = '" + entity.SHIFT_BREAK_START + "' " +
                           " ,SHIFT_BREAK_END = '" + entity.SHIFT_BREAK_END + "' " +
                           " ,SHIFT_BREAK_HRS = '" + entity.SHIFT_BREAK_HRS + "' " +
                           " ,SHIFT_WORKHRS = '" + entity.SHIFT_WORKHRS + "' " +
                           " ,SHIFT_HALFDAYWORKHRS = '" + entity.SHIFT_HALFDAYWORKHRS + "' " +
                           " ,SHIFT_FLAG_ADD_BREAK = '" + entity.SHIFT_FLAG_ADD_BREAK + "' " +
                           " ,SHIFT_WEEKEND_DIFF_TIME = '" + entity.SHIFT_WEEKEND_DIFF_TIME + "' " +
                           " ,SHIFT_WEEKEND_START = '" + entity.SHIFT_WEEKEND_START + "' " +
                           " ,SHIFT_WEEKEND_END = '" + entity.SHIFT_WEEKEND_END + "' " +
                           " ,SHIFT_WEEKEND_BREAK_START = '" + entity.SHIFT_WEEKEND_BREAK_START + "' " +
                           " ,SHIFT_WEEKEND_BREAK_END = '" + entity.SHIFT_WEEKEND_BREAK_END + "' " +
                           " ,SHIFT_EARLY_SEARCH_HRS = '" + entity.SHIFT_EARLY_SEARCH_HRS + "' " +
                           " ,SHIFT_LATE_SEARCH_HRS = '" + entity.SHIFT_LATE_SEARCH_HRS + "' " +

                          " ,COMPANY_ID = '" + entity.COMPANY_ID + "'" +
                          " ,SHIFT_MODIFIEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "', SHIFT_MODIFIEDBY='" + entity.SHIFT_MODIFIEDBY + "'" +
                          " where SHIFT_ID =  " + entity.SHIFT_ID;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " update ta_shift set SHIFT_ISDELETED = 1,SHIFT_DELETEDDATE='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "',SHIFT_DELETEDBY='" + user + "'" +
                           " where SHIFT_ID =  " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public IQueryable<ShiftDto> Get(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select * from ta_shift where SHIFT_ISDELETED =0 ";
            }
            else
            {
                query = " select * from ta_shift where SHIFT_ISDELETED =0 and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }
            
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ShiftDto> shiftList = new List<ShiftDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    ShiftDto shift = new ShiftDto();
                    shift.SHIFT_ID = Convert.ToInt32(dr["SHIFT_ID"]);
                    shift.SHIFT_CODE = Convert.ToString(dr["SHIFT_CODE"]);
                    shift.SHIFT_DESCRIPTION = Convert.ToString(dr["SHIFT_DESCRIPTION"]);
                    shift.SHIFT_START = Convert.ToDateTime(dr["SHIFT_START"]).TimeOfDay;
                    shift.SHIFT_END = Convert.ToDateTime(dr["SHIFT_END"]).TimeOfDay;
                    shift.SHIFT_BREAK_START = Convert.ToDateTime(dr["SHIFT_BREAK_START"]).TimeOfDay;
                    shift.SHIFT_BREAK_END = Convert.ToDateTime(dr["SHIFT_BREAK_END"]).TimeOfDay;
                    shift.SHIFT_BREAK_HRS = Convert.ToDateTime(dr["SHIFT_BREAK_HRS"]).TimeOfDay;
                    shift.SHIFT_WORKHRS = Convert.ToDateTime(dr["SHIFT_WORKHRS"]).TimeOfDay;
                    shift.SHIFT_HALFDAYWORKHRS = Convert.ToDateTime(dr["SHIFT_HALFDAYWORKHRS"]).TimeOfDay;
                    shift.SHIFT_TYPE = Convert.ToString(dr["SHIFT_TYPE"]);
                    shiftList.Add(shift);
                }
            }

            return shiftList.AsQueryable();
        }

        public Task<Shift> GetSingle(int id)
        {
            string query = "select * from ta_shift where SHIFT_ID = " + id;



            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForShift = dataSet.Tables[0];

            Shift shift = new Shift();

            if (dataTableForShift.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForShift.Rows)
                {

                    shift.SHIFT_ID = Convert.ToInt32(dr["SHIFT_ID"]);
                    shift.SHIFT_CODE = Convert.ToString(dr["SHIFT_CODE"]);
                    shift.SHIFT_DESCRIPTION = Convert.ToString(dr["SHIFT_DESCRIPTION"]);
                    shift.SHIFT_ALLOCATION_TYPE = Convert.ToString(dr["SHIFT_ALLOCATION_TYPE"]);
                    shift.SHIFT_AUTO_SEARCH_START = Convert.ToDateTime(dr["SHIFT_AUTO_SEARCH_START"]).TimeOfDay;
                    shift.SHIFT_AUTO_SEARCH_END = Convert.ToDateTime(dr["SHIFT_AUTO_SEARCH_END"]).TimeOfDay;
                    shift.SHIFT_TYPE = Convert.ToString(dr["SHIFT_TYPE"]);
                    shift.SHIFT_START = Convert.ToDateTime(dr["SHIFT_START"]).TimeOfDay;
                    shift.SHIFT_END = Convert.ToDateTime(dr["SHIFT_END"]).TimeOfDay;
                    shift.SHIFT_BREAK_START = Convert.ToDateTime(dr["SHIFT_BREAK_START"]).TimeOfDay;
                    shift.SHIFT_BREAK_END = Convert.ToDateTime(dr["SHIFT_BREAK_END"]).TimeOfDay;
                    shift.SHIFT_BREAK_HRS = Convert.ToDateTime(dr["SHIFT_BREAK_HRS"]).TimeOfDay;
                    shift.SHIFT_WORKHRS = Convert.ToDateTime(dr["SHIFT_WORKHRS"]).TimeOfDay;
                    shift.SHIFT_HALFDAYWORKHRS = Convert.ToDateTime(dr["SHIFT_HALFDAYWORKHRS"]).TimeOfDay;
                    shift.SHIFT_FLAG_ADD_BREAK = Convert.ToBoolean(dr["SHIFT_FLAG_ADD_BREAK"]);
                    shift.SHIFT_WEEKEND_DIFF_TIME = Convert.ToBoolean(dr["SHIFT_WEEKEND_DIFF_TIME"]);

                    shift.SHIFT_WEEKEND_START = Convert.ToDateTime(dr["SHIFT_WEEKEND_START"]).TimeOfDay;
                    shift.SHIFT_WEEKEND_END = Convert.ToDateTime(dr["SHIFT_WEEKEND_END"]).TimeOfDay;
                    shift.SHIFT_WEEKEND_BREAK_START = Convert.ToDateTime(dr["SHIFT_WEEKEND_BREAK_START"]).TimeOfDay;
                    shift.SHIFT_WEEKEND_BREAK_END = Convert.ToDateTime(dr["SHIFT_WEEKEND_BREAK_END"]).TimeOfDay;
                    shift.SHIFT_EARLY_SEARCH_HRS = Convert.ToDateTime(dr["SHIFT_EARLY_SEARCH_HRS"]).TimeOfDay;
                    shift.SHIFT_LATE_SEARCH_HRS = Convert.ToDateTime(dr["SHIFT_LATE_SEARCH_HRS"]).TimeOfDay;
                    shift.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);

                }
            }


            return Task.Run(() =>
            {
                return shift;
            });
        }
    }
}