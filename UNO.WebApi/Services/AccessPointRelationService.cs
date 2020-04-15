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
    public interface IAccessPointRelationService
    {
        Task Create(AccessPointRelation entity, string ipaddress, int activeuser);

        Task Delete(int id, string ipaddress, int activeuser);
    }
    public class AccessPointRelationService : IAccessPointRelationService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public AccessPointRelationService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task Create(AccessPointRelation entity, string ipaddress, int activeuser)
        {
            string query = " Insert into ACS_ACCESSPOINT_RELATION(AP_ID,READER_ID,DOOR_ID,AP_CONTROLLER_ID,APR_ISDELETED,AP_FLAG) " +
                           " Values(" + entity.AP_ID + "," + entity.READER_ID + "," + entity.DOOR_ID + "," + entity.AP_CONTROLLER_ID + ",0,0)";
            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task Delete(int id, string ipaddress, int activeuser)
        {
            string query = " update ACS_ACCESSPOINT_RELATION set APR_ISDELETED = 0 " +
                           " where AP_CONTROLLER_ID= " + id;
            await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
    }
}