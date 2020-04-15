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
    public interface IDoorService
    {
        Task Create(AcsDoor entity, string ipaddress, int activeuser);
        Task Edit(AcsDoor entity, string ipaddress, int activeuser);
        Task Delete(int id, string ipaddress, int activeuser);
    }
    public class DoorService : IDoorService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public DoorService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task Create(AcsDoor entity, string ipaddress, int activeuser)
        {
            string query = " Insert into ACS_DOOR (DOOR_ID,CTLR_ID,DOOR_TYPE,DOOR_OPEN_DURATION,DOOR_FEEDBACK_DURATION,DOOR_ISDELETED,READER_ID,AP_FLAG) " +
                           " values (" + entity.DOOR_ID + "," + entity.CTLR_ID + ",'" + entity.DOOR_TYPE + "', " +
                           " '" + entity.DOOR_OPEN_DURATION + "','" + entity.DOOR_FEEDBACK_DURATION + "',0," + entity.READER_ID + ",0) ";
            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress,activeuser);
        }


        public async Task Edit(AcsDoor entity, string ipaddress, int activeuser)
        {
            string query = " update ACS_DOOR set DOOR_TYPE = '" + entity.DOOR_TYPE + "',DOOR_OPEN_DURATION = '" + entity.DOOR_OPEN_DURATION + "',DOOR_FEEDBACK_DURATION = '" + entity.DOOR_FEEDBACK_DURATION + "'" +
                           " where DOOR_ID = " + entity.DOOR_ID + " and CTLR_ID = " + entity.CTLR_ID + "";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task Delete(int id, string ipaddress, int activeuser)
        {
            string query = " update ACS_DOOR set DOOR_ISDELETED = 1 " +
                           " where DOOR_ISDELETED=0 and CTLR_ID = " + id + "";

            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}