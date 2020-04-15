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
    public interface IControllerService
    {
        Task<int> Create(AcsController entity, string ipaddress, int activeuser);
        Task<int> Edit(AcsController entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        IQueryable<AcsControllerDto> Get(int id);
        Task<AcsController> GetSingle(int id);
        Task<bool> GetControllerByID(string controllerId, string companyId);
        Task<bool> GetControllerByIP(string controllerIp, string companyId);
    }
    public class ControllerService : IControllerService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IReaderService _readerService;
        private IUserService _userService;
        public ControllerService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IReaderService readerService
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _readerService = readerService;
            _userService = userService;
        }
        public async Task<int> Create(AcsController entity, string ipaddress, int activeuser)
        {
            string query = " Insert into ACS_CONTROLLER (CTLR_ID,CTLR_DESCRIPTION,CTLR_TYPE,CTLR_IP,CTLR_MAC_ID," +
                           " CTLR_INCOMING_PORT,CTLR_OUTGOING_PORT,CTLR_FIRMWARE_VERSION_NO,CTLR_HARDWARE_VERSION_NO, " +
                           " CTLR_CHK_APB,CTLR_APB_TYPE,CTLR_APB_TIME,CTLR_AUTHENTICATION_MODE,CTLR_CHK_TOC,CTLR_ISDELETED,CLTR_FOR_TA,CTLR_KEY_PAD,CTLR_LOCATION_ID,COMPANY_ID,CTLR_CREATEDDATE,CTLR_CREATEDBY) " +
                           " Values ('" + entity.CTLR_ID + "','" + entity.CTLR_DESCRIPTION + "','" + entity.CTLR_TYPE + "', " +
                           " '" + entity.CTLR_IP + "' ,'" + entity.CTLR_MAC_ID + "','" + entity.CTLR_INCOMING_PORT + "'," +
                           " '" + entity.CTLR_OUTGOING_PORT + "' , '" + entity.CTLR_FIRMWARE_VERSION_NO + "', '" + entity.CTLR_HARDWARE_VERSION_NO + "', " +
                           " '" + entity.CTLR_CHK_APB + "','" + entity.CTLR_APB_TYPE + "','" + entity.CTLR_APB_TIME + "','" + entity.CTLR_AUTHENTICATION_MODE + "' ,'" + entity.CTLR_CHK_TOC + "',0,'" + entity.CLTR_FOR_TA + "','" + entity.CTLR_KEY_PAD + "','" + entity.CTLR_LOCATION_ID + "','" + entity.COMPANY_ID + "','" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "','" + entity.CTLR_CREATEDBY + "') ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task<bool> GetControllerByID(string controllerId, string companyId)
        {
            string query = "Select count(1) from ACS_CONTROLLER where CTLR_ID = '" + controllerId + "' and COMPANY_ID='" + companyId + "' and CTLR_ISDELETED=0";
            return await _DatabaseHelper.GetScalarValue(query, CommandType.Text) == 0 ? false : true;
        }


        public async Task<bool> GetControllerByIP(string controllerIp, string companyId)
        {
            string query = "Select count(1) from ACS_CONTROLLER where CTLR_IP = '" + controllerIp + "' and COMPANY_ID='" + companyId + "' and CTLR_ISDELETED=0";
            return await _DatabaseHelper.GetScalarValue(query, CommandType.Text) == 0 ? false : true;
        }


        public Task<AcsController> GetSingle(int id)
        {
            string query = "select * from acs_controller where ID = " + id;
            //query += " ; ";

            query += " ; select apr.AP_ID,CTLR_ID,door.READER_ID,door.DOOR_ID,DOOR_TYPE,DOOR_OPEN_DURATION,DOOR_FEEDBACK_DURATION from ACS_DOOR door"
                  + " join ACS_ACCESSPOINT_RELATION apr on door.DOOR_ID=apr.DOOR_ID and door.READER_ID=apr.READER_ID "
                  + " where CTLR_ID in (select ID from acs_controller where ID =" + id + ") and AP_CONTROLLER_ID in (select ID from acs_controller where ID =" + id + ") ";

            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForController = dataSet.Tables[0];

            AcsController acsController = new AcsController();

            if (dataTableForController.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForController.Rows)
                {

                    acsController.ID = Convert.ToInt64(dr["ID"]);
                    acsController.CTLR_ID = Convert.ToInt32(dr["CTLR_ID"]);
                    acsController.CTLR_DESCRIPTION = Convert.ToString(dr["CTLR_DESCRIPTION"]);
                    acsController.CTLR_TYPE = Convert.ToString(dr["CTLR_TYPE"]);
                    acsController.CTLR_IP = Convert.ToString(dr["CTLR_IP"]);
                    acsController.CTLR_MAC_ID = Convert.ToString(dr["CTLR_MAC_ID"]);
                    acsController.CTLR_INCOMING_PORT = Convert.ToString(dr["CTLR_INCOMING_PORT"]);
                    acsController.CTLR_OUTGOING_PORT = Convert.ToString(dr["CTLR_OUTGOING_PORT"]);
                    acsController.CTLR_FIRMWARE_VERSION_NO = Convert.ToString(dr["CTLR_FIRMWARE_VERSION_NO"]);
                    acsController.CTLR_HARDWARE_VERSION_NO = Convert.ToString(dr["CTLR_HARDWARE_VERSION_NO"]);
                    acsController.CTLR_CHK_APB = Convert.ToString(dr["CTLR_CHK_APB"]);
                    acsController.CTLR_APB_TYPE = Convert.ToString(dr["CTLR_APB_TYPE"]);
                    acsController.CTLR_APB_TIME = Convert.ToString(dr["CTLR_APB_TIME"]);
                    acsController.CTLR_AUTHENTICATION_MODE = Convert.ToString(dr["CTLR_AUTHENTICATION_MODE"]);
                    acsController.CTLR_CHK_TOC = Convert.ToBoolean(dr["CTLR_CHK_TOC"]);
                    acsController.CTLR_EVENTS_STORED = Convert.ToString(dr["CTLR_EVENTS_STORED"]);
                    acsController.CTLR_EVENTS_STORED = Convert.ToString(dr["CTLR_EVENTS_STORED"]);
                    acsController.CLTR_FOR_TA = Convert.ToBoolean(dr["CLTR_FOR_TA"]);
                    acsController.CTLR_KEY_PAD = Convert.ToBoolean(dr["CTLR_KEY_PAD"]);
                    acsController.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    acsController.CTLR_ISDELETED = Convert.ToBoolean(dr["CTLR_ISDELETED"]);
                }
            }

            acsController.Readers = _readerService.GetReaderByControllerIdAndCompanyId(id, Convert.ToInt32(acsController.COMPANY_ID));
            var dataTableForAccessPointDetails = dataSet.Tables[1];
            List<AccessPointDetails> accessPointDetails = new List<AccessPointDetails>();
            if (dataTableForAccessPointDetails.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForAccessPointDetails.Rows)
                {
                    AccessPointDetails accessPointDetail = new AccessPointDetails();
                    accessPointDetail.AP_ID = Convert.ToDecimal(dr["AP_ID"]);
                    accessPointDetail.CTLR_ID = Convert.ToInt32(dr["CTLR_ID"]);
                    accessPointDetail.READER_ID = Convert.ToInt32(dr["READER_ID"]);
                    accessPointDetail.DOOR_ID = Convert.ToInt32(dr["DOOR_ID"]);
                    accessPointDetail.DOOR_OPEN_DURATION = Convert.ToString(dr["DOOR_OPEN_DURATION"]);
                    accessPointDetail.DOOR_FEEDBACK_DURATION = Convert.ToString(dr["DOOR_FEEDBACK_DURATION"]);
                    accessPointDetail.DOOR_TYPE = Convert.ToString(dr["DOOR_TYPE"]);
                    accessPointDetails.Add(accessPointDetail);
                }
            }

            acsController.AccessPointDetails = accessPointDetails;

            return Task.Run(() =>
            {
                return acsController;
            });
        }


        public IQueryable<AcsControllerDto> Get(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select * from acs_controller where CTLR_ISDELETED =0 ";
            }
            else
            {
                query = " select * from acs_controller where CTLR_ISDELETED =0 and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<AcsControllerDto> acsList = new List<AcsControllerDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AcsControllerDto acs = new AcsControllerDto();

                    acs.ID = Convert.ToInt64(dr["ID"]);
                    acs.CTLR_ID = Convert.ToInt32(dr["CTLR_ID"]);
                    acs.CTLR_DESCRIPTION = Convert.ToString(dr["CTLR_DESCRIPTION"]);
                    acs.CTLR_TYPE = Convert.ToString(dr["CTLR_TYPE"]);
                    acs.CTLR_IP = Convert.ToString(dr["CTLR_IP"]);
                    acs.CTLR_CONN_STATUS = Convert.ToString(dr["CTLR_CONN_STATUS"]);
                    acs.CTLR_INACTIVE_DATETIME = Convert.ToString(dr["CTLR_INACTIVE_DATETIME"]);
                    acs.CTLR_EVENTS_STORED = Convert.ToString(dr["CTLR_EVENTS_STORED"]);
                    acs.CTLR_CURRENT_USER_CNT = Convert.ToString(dr["CTLR_CURRENT_USER_CNT"]);
                    acs.CTLR_KEY_PAD = Convert.ToBoolean(dr["CTLR_KEY_PAD"]) ? "Enabled" : "Disabled";
                    acs.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                    acs.CTLR_FIRMWARE_VERSION_NO = Convert.ToString(dr["CTLR_FIRMWARE_VERSION_NO"]);
                    acsList.Add(acs);
                }
            }

            return acsList.AsQueryable();
        }

        public async Task<int> Edit(AcsController entity, string ipaddress, int activeuser)
        {
            string query = " update ACS_CONTROLLER set CTLR_DESCRIPTION = '" + entity.CTLR_DESCRIPTION + "'" +
                           " ,COMPANY_ID = " + entity.COMPANY_ID + "" +
                           " ,CTLR_IP = '" + entity.CTLR_IP + "' " +
                           " ,CTLR_CHK_APB = '" + entity.CTLR_CHK_APB + "',CTLR_APB_TYPE='" + entity.CTLR_APB_TYPE + "'" +
                           " ,CTLR_APB_TIME='" + entity.CTLR_APB_TIME + "',CTLR_AUTHENTICATION_MODE='" + entity.CTLR_AUTHENTICATION_MODE + "'" +
                           " ,CTLR_CHK_TOC='" + entity.CTLR_CHK_TOC + "',CLTR_FOR_TA='" + entity.CLTR_FOR_TA + "'" +
                           " ,CTLR_KEY_PAD='" + entity.CTLR_KEY_PAD + "' " +
                           " ,CTLR_MODIFIEDBY='" + entity.CTLR_MODIFIEDBY + "'" +
                           " ,CTLR_MODIFIEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           " where Id =  " + entity.ID;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }


        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " update ACS_CONTROLLER set CTLR_ISDELETED = 1" +
                           " ,CTLR_DELETEDBY='" + user + "'" +
                           " ,CTLR_DELETEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           " where Id =  " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }
    }
}