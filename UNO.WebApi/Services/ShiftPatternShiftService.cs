using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;

namespace UNO.WebApi.Services
{
    public interface IShiftPatternShiftService
    {
        Task<int> Create(int shiftId, int shiftPatternId, int shiftorder, string ipaddress, int activeuser);
        Task<int> Edit(int shiftId, int shiftPatternId, int shiftorder, string ipaddress, int activeuser);
        Task<int> Delete(int shiftPatternId, string ipaddress, int activeuser);
    }
    public class ShiftPatternShiftService : IShiftPatternShiftService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public ShiftPatternShiftService(UnitOfWork unitOfWork
            , DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task<int> Create(int shiftId, int shiftPatternId, int shiftorder, string ipaddress, int activeuser)
        {
            string query = " Insert into SHIFT_SHIFT_PATTERN (SHIFT_ID,SHIFT_PATTERN_ID,shiftorder) " +
                         " Values ('" + shiftId + "','" + shiftPatternId + "','" + shiftorder + "') ";
            //query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }


        public async Task<int> Edit(int shiftId, int shiftPatternId, int shiftorder, string ipaddress, int activeuser)
        {
            string query = " Insert into SHIFT_SHIFT_PATTERN (SHIFT_ID,SHIFT_PATTERN_ID,shiftorder) " +
                          " Values ('" + shiftId + "','" + shiftPatternId + "','" + shiftorder + "') ";
            //query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task<int> Delete(int shiftPatternId, string ipaddress, int activeuser)
        {
            string query = " delete SHIFT_SHIFT_PATTERN where SHIFT_PATTERN_ID = '" + shiftPatternId + "'; ";
            //query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}