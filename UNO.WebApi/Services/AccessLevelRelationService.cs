using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IAccessLevelRelationService
    {
        Task<int> Create(AccessLevelRelation entity, string ipaddress, int activeuser);
        Task<int> Edit(AccessLevelRelation entity);
        Task<int> Delete(int id, string user);
        Task<int> DeleteByAlId(long id, string ipaddress, int activeuser);
        Task<string> DeleteByZoneId(int id, string ipaddress, int activeuser);
        Task<dynamic> GetAlIdByZoneId(int id);
        IQueryable<AccessLevelRelation> Get(int id);
        IQueryable<AccessLevelRelationEdit> GetAccessRelationByZoneId(int id, string activity, string dt);
        Task<AccessLevelRelation> GetSingle(int id);
        Task<string> GetAccessArray(int id, long accessLevelId, int controllerId, int readerId);
    }
    public class AccessLevelRelationService : IAccessLevelRelationService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public AccessLevelRelationService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task<int> Create(AccessLevelRelation entity,string ipaddress,int activeuser)
        {

            var zoneId = entity.ZoneId.ToString();
            if (entity.ZoneId == null)
            {
                zoneId = "null";
            }
            string query = " Insert into ACS_ACCESSLEVEL_RELATION (AL_ID,RD_ZN_ID,AL_ENTITY_TYPE,CONTROLLER_ID" +
                           " ,AccesLevelArray,ZoneId )" +
                           " Values ('" + entity.AL_ID + "','" + entity.RD_ZN_ID + "','" + entity.AL_ENTITY_TYPE + "', " +
                           " '" + entity.CONTROLLER_ID + "' ,'" + entity.AccesLevelArray + "'," + zoneId + "" +
                           " ) ;";

            //query += " select @@Identity ;";
            query += " UPDATE ACS_ACCESSLEVEL_RELATION set AccesLevelArray='" + entity.AccesLevelArray + "' " +
                     " WHERE AL_ID='" + entity.AL_ID + "' AND CONTROLLER_ID=" + entity.CONTROLLER_ID + " ";

            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public Task<int> Edit(AccessLevelRelation entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id, string user)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteByAlId(long id, string ipaddress, int activeuser)
        {
            string query = " update ACS_ACCESSLEVEL_RELATION set ALR_ISDELETED = 1" +
                           " ,ALR_DELETEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           " where ALR_ISDELETED = 0 and AL_ID =  " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public IQueryable<AccessLevelRelation> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AccessLevelRelation> GetSingle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAccessArray(int id, long accessLevelId, int controllerId, int readerId)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select AccesLevelArray from ACS_ACCESSLEVEL_RELATION  rel join ACS_CONTROLLER acs on acs.CTLR_ID =rel.CONTROLLER_ID WHERE CONTROLLER_ID=" + controllerId + " and AL_ID=" + accessLevelId + " ";
            }
            else
            {
                query = " select AccesLevelArray from ACS_ACCESSLEVEL_RELATION  rel join ACS_CONTROLLER acs on acs.CTLR_ID =rel.CONTROLLER_ID WHERE CONTROLLER_ID=" + controllerId + " and AL_ID=" + accessLevelId + " and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }
            StringBuilder accLevelAraay = new StringBuilder();
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            if (dataTable.Rows.Count > 0)
            {
                accLevelAraay.Append(dataTable.Rows[0]["AccesLevelArray"].ToString());
            }
            else
            {
                accLevelAraay.Append("00000000");
            }

            int arrayPos = Convert.ToInt32(readerId);

            accLevelAraay.Remove(accLevelAraay.Length - arrayPos, 1);
            accLevelAraay.Insert(accLevelAraay.Length - arrayPos + 1, '1');
            return Task.Run(() => accLevelAraay.ToString());
        }


        public async Task<string> DeleteByZoneId(int id, string ipaddress, int activeuser)
        {
            var dt = DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss");
            string query = " update ACS_ACCESSLEVEL_RELATION set ALR_ISDELETED = 1" +
                           " ,ALR_DELETEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                           " where ALR_ISDELETED = 0 and ZoneId =  " + id + "; ";

            query += "select @@rowcount";

            var x = await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
            return dt;
        }


        public async Task<dynamic> GetAlIdByZoneId(int id)
        {
            string query = "select distinct al_id from acs_accesslevel_relation where zoneid=" + id;


            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text);
            return x;
        }


        public IQueryable<AccessLevelRelationEdit> GetAccessRelationByZoneId(int id, string activity, string dt)
        {
            string query = string.Empty;
            if (activity.ToLower() == "delete")
            {
                query = " select distinct AL_ID,CONTROLLER_ID,zoneid from acs_accesslevel_relation " +
                        " where zoneid=" + id + " and ALR_ISDELETED=1 and ALR_DELETEDDATE='" + dt + "'" +
                        " except " +
                        " select distinct AL_ID,CONTROLLER_ID,zoneid from acs_accesslevel_relation " +
                        " where zoneid=" + id + " and ALR_ISDELETED=0 ";
            }
            else if (activity.ToLower() == "add")
            {
                query = " select distinct AL_ID,CONTROLLER_ID,zoneid from acs_accesslevel_relation " +
                        " where zoneid=" + id + " and ALR_ISDELETED=0 " +
                        " except " +
                        " select distinct AL_ID,CONTROLLER_ID,zoneid from acs_accesslevel_relation " +
                        " where zoneid=" + id + " and ALR_ISDELETED=1 and ALR_DELETEDDATE='" + dt + "'";
            }

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<AccessLevelRelationEdit> accessLevelRelationEdits = new List<AccessLevelRelationEdit>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AccessLevelRelationEdit accessLevelRelationEdit = new AccessLevelRelationEdit();
                    accessLevelRelationEdit.AL_ID = Convert.ToInt32(dr["AL_ID"]);
                    accessLevelRelationEdit.CONTROLLER_ID = Convert.ToInt32(dr["CONTROLLER_ID"]);
                    accessLevelRelationEdit.ZoneId = Convert.ToInt32(dr["ZoneId"]);

                    accessLevelRelationEdits.Add(accessLevelRelationEdit);
                }
            }

            return accessLevelRelationEdits.AsQueryable();
        }
    }
}