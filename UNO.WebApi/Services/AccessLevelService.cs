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
    public interface IAccessLevelService
    {
        Task<int> Create(AccessLevel entity, string ipaddress, int activeuser);
        Task<int> Edit(AccessLevel entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        IQueryable<AccessLevelDto> Get(int id);
        Task<AccessLevel> GetSingle(int id);
        IQueryable<int> GetControllersByAlId(long id);
    }
    public class AccessLevelService : IAccessLevelService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public AccessLevelService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task<int> Create(AccessLevel entity, string ipaddress, int activeuser)
        {
            string query = " Insert into ACS_ACCESSLEVEL (AL_DESCRIPTION,AL_TIMEZONE_ID,AL_ISDELETED) " +
                          " Values ('" + entity.AL_DESCRIPTION + "','" + entity.AL_TIMEZONE_ID + "',0) ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(AccessLevel entity, string ipaddress, int activeuser)
        {
            string query = " update ACS_ACCESSLEVEL set AL_DESCRIPTION = '" + entity.AL_DESCRIPTION + "'" +
                            " ,AL_TIMEZONE_ID = " + entity.AL_TIMEZONE_ID + "" +
                            " where AL_ID =  " + entity.AL_ID;

            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " update ACS_ACCESSLEVEL set AL_ISDELETED = 1" +
                           " ,AL_DELETEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           " where AL_ID =  " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public IQueryable<AccessLevelDto> Get(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select distinct al.al_id,al_description from ACS_ACCESSLEVEL al where AL_ISDELETED =0 ";
            }
            else
            {
                query = " select distinct al.al_id,al_description from ACS_ACCESSLEVEL al  join ACS_ACCESSLEVEL_RELATION alr on al.AL_ID=alr.AL_ID join acs_controller acs on alr.CONTROLLER_ID =acs.CTLR_ID and AL_ISDELETED =0 and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<AccessLevelDto> accessLevelList = new List<AccessLevelDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AccessLevelDto accessLevel = new AccessLevelDto();
                    accessLevel.AL_ID = Convert.ToInt64(dr["AL_ID"]);
                    accessLevel.AL_DESCRIPTION = Convert.ToString(dr["AL_DESCRIPTION"]);
                    accessLevelList.Add(accessLevel);
                }
            }

            return accessLevelList.AsQueryable();
        }

        public Task<AccessLevel> GetSingle(int id)
        {
            string query = "select * from ACS_ACCESSLEVEL where AL_ID = " + id;
            //query += " ; ";

            query += " ; select AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,ZoneId from ACS_ACCESSLEVEL_RELATION  where ALR_ISDELETED =0 and AL_ID = " + id;

            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForAccessLevel = dataSet.Tables[0];

            AccessLevel accessLevel = new AccessLevel();

            if (dataTableForAccessLevel.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForAccessLevel.Rows)
                {

                    accessLevel.AL_ID = Convert.ToInt64(dr["AL_ID"]);
                    accessLevel.AL_TIMEZONE_ID = Convert.ToInt32(dr["AL_TIMEZONE_ID"]);
                    accessLevel.AL_DESCRIPTION = Convert.ToString(dr["AL_DESCRIPTION"]);
                }
            }

            //accessLevel.Readers = _readerService.GetReaderByControllerIdAndCompanyId(id, Convert.ToInt32(accessLevel.COMPANY_ID));
            var dataTableForAccessLevelRelations = dataSet.Tables[1];

            AccessLevelRelationInfo accessLevelRelation = new AccessLevelRelationInfo();
            List<int> entity = new List<int>();
            if (dataTableForAccessLevelRelations.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForAccessLevelRelations.Rows)
                {
                    accessLevelRelation.AL_ENTITY_TYPE = Convert.ToString(dr["AL_ENTITY_TYPE"]);

                    if (Convert.ToString(dr["AL_ENTITY_TYPE"]).ToUpper() == "Z")
                    {
                        entity.Add(Convert.ToInt32(dr["ZoneId"]));
                    }
                    else
                    {
                        entity.Add(Convert.ToInt32(dr["RD_ZN_ID"]));
                    }
                }
                accessLevelRelation.AccessLevelReaders = entity;
            }

            accessLevel.AccessLevelRelation = accessLevelRelation;

            return Task.Run(() =>
            {
                return accessLevel;
            });
        }


        public IQueryable<int> GetControllersByAlId(long id)
        {
            string query = " select distinct CONTROLLER_ID from ACS_ACCESSLEVEL_RELATION  where ALR_ISDELETED =0 and AL_ID = " + id;
            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForController = dataSet.Tables[0];
            List<int> entity = new List<int>();
            if (dataTableForController.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForController.Rows)
                {
                    entity.Add(Convert.ToInt32(dr["CONTROLLER_ID"]));
                }
            }
            return entity.AsQueryable();
        }
    }
}