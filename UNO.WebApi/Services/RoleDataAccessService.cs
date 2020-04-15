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
    public interface IRoleDataAccessService
    {
        Task<int> Create(RoleDataAccess entity, string ipaddress, int activeuser);
        Task<int> Edit(RoleDataAccess entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        Task<int> DeleteByUserId(int id, string ipaddress, int activeuser);
        IQueryable<RoleDataAccess> Get(int id);
        IQueryable<MappedEntityId> GetMappedEntityByUserId(int id);
        Task<RoleDataAccess> GetSingle(int id);
    }
    public class RoleDataAccessService : IRoleDataAccessService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public RoleDataAccessService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task<int> Create(RoleDataAccess entity, string ipaddress, int activeuser)
        {
            string query = " INSERT INTO ENT_ROLE_DATA_ACCESS (" +
                           " USER_CODE,COMMON_TYPES_ID,MAPPED_ENTITY_ID,IS_SYNC)" +
                           " VALUES('" + entity.USER_CODE + "','" + entity.COMMON_TYPES_ID + "','" + entity.MAPPED_ENTITY_ID + "',0) ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(RoleDataAccess entity, string ipaddress, int activeuser)
        {
            string query = " UPDATE ENT_ROLE_DATA_ACCESS  SET USER_CODE = '" + entity.USER_CODE + "' " +
                           " ,COMMON_TYPES_ID = '" + entity.COMMON_TYPES_ID + "'" +
                           " ,MAPPED_ENTITY_ID = '" + entity.MAPPED_ENTITY_ID + "'" +
                           " WHERE DATA_ACCESS_ID='" + entity.DATA_ACCESS_ID + "'";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " DELETE ENT_ROLE_DATA_ACCESS " +
                           " WHERE DATA_ACCESS_ID='" + id + "'";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> DeleteByUserId(int id, string ipaddress, int activeuser)
        {
            string query = " DELETE ENT_ROLE_DATA_ACCESS " +
                           " WHERE USER_CODE='" + id + "'";

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public IQueryable<RoleDataAccess> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MappedEntityId> GetMappedEntityByUserId(int id)
        {
            string query = string.Empty;
            query = " select COMMON_TYPES, COMPANY_NAME as entityname, com.COMPANY_ID as entityId " +
                    " from ENT_ROLE_DATA_ACCESS da " +
                    " join ent_org_common_types ct on da.COMMON_TYPES_ID=ct.COMMON_TYPES_ID " +
                    " join ent_company com on da.MAPPED_ENTITY_ID = com.COMPANY_ID and ct.COMMON_TYPES='Company' " +
                    " where USER_CODE=" + id + " " +
                    " union " +
                    " select COMMON_TYPES, EPD_FIRST_NAME + ' ' + EPD_LAST_NAME as entityname,EMPLOYEE_ID as entityId " +
                    " from ENT_ROLE_DATA_ACCESS da " +
                    " join ent_org_common_types ct on da.COMMON_TYPES_ID=ct.COMMON_TYPES_ID " +
                    " join ENT_EMPLOYEE_DTLS emp on da.MAPPED_ENTITY_ID = emp.EMPLOYEE_ID and ct.COMMON_TYPES='Employee' " +
                    " where USER_CODE=" + id + " " +
                    " union " +
                    " select COMMON_TYPES, OCE_DESCRIPTION as entityname,ID as entityId " +
                    " from ENT_ROLE_DATA_ACCESS da " +
                    " join ent_org_common_types ct on da.COMMON_TYPES_ID=ct.COMMON_TYPES_ID " +
                    " join ENT_ORG_COMMON_ENTITIES org on da.MAPPED_ENTITY_ID=org.ID and ct.COMMON_TYPES<>'Company' and ct.COMMON_TYPES<>'Employee' " +
                    " where USER_CODE=" + id + " ";

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<MappedEntityId> acsList = new List<MappedEntityId>();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    MappedEntityId acs = new MappedEntityId();

                    acs.CommonTypes = Convert.ToString(dr["COMMON_TYPES"]);
                    acs.MAPPED_ENTITY_NAME = Convert.ToString(dr["entityname"]);
                    acs.MAPPED_ENTITY_ID = Convert.ToInt32(dr["entityId"]);
                    acsList.Add(acs);
                }
            }
            return acsList.AsQueryable();
        }

        public Task<RoleDataAccess> GetSingle(int id)
        {
            throw new NotImplementedException();
        }
    }
}