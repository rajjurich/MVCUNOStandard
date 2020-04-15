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
    public interface IZoneService
    {
        Task<int> Create(Zone entity, string ipaddress, int activeuser);
        Task<int> Edit(Zone entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string user, string ipaddress, int activeuser);
        IQueryable<ZoneDto> Get(int id);
        Task<Zone> GetSingle(int id);
    }
    public class ZoneService : IZoneService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private IUserService _userService;
        public ZoneService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            , IUserService userService)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _userService = userService;
        }
        public async Task<int> Create(Zone entity, string ipaddress, int activeuser)
        {
            string query = " Insert into ZONE (ZONE_DESCRIPTION,COMPANY_ID,ZONE_CREATEDBY,ZONE_CREATEDDATE) " +
                          " Values ('" + entity.ZONE_DESCRIPTION + "','" + entity.COMPANY_ID + "','" + entity.ZONE_CREATEDBY + "','" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "') ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public async Task<int> Edit(Zone entity, string ipaddress, int activeuser)
        {
            string query = " update ZONE set ZONE_DESCRIPTION =  '" + entity.ZONE_DESCRIPTION + "',ZONE_MODIFIEDBY='" + entity.ZONE_MODIFIEDBY + "',ZONE_MODIFIEDDATE= '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "'" +
                         " where ZONE_ID =  " + entity.ZONE_ID;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string user, string ipaddress, int activeuser)
        {
            string query = " update ZONE set ZONE_ISDELETED = 1, ZONE_DELETEDDATE = '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "',ZONE_DELETEDBY='" + user + "' " +
                        " where ZONE_ID =  " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public IQueryable<ZoneDto> Get(int id)
        {
            string query = string.Empty;
            User user = _userService.GetSingleUser(id);
            if (user.USER_CODE == "superuser")
            {
                query = " select * from ZONE where ZONE_ISDELETED =0 ";
            }
            else
            {
                query = " select * from ZONE where ZONE_ISDELETED =0 and COMPANY_ID in( " +
                        CustomQueryies.Queries.RoleDataAccessQuery(id) + " ) ";
            }
            
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ZoneDto> zoneDtos = new List<ZoneDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    ZoneDto zoneDto = new ZoneDto();
                    zoneDto.ZONE_ID = Convert.ToInt32(dr["ZONE_ID"]);
                    zoneDto.ZONE_DESCRIPTION = Convert.ToString(dr["ZONE_DESCRIPTION"]);

                    zoneDtos.Add(zoneDto);
                }
            }

            return zoneDtos.AsQueryable();
        }

        public Task<Zone> GetSingle(int id)
        {
            string query = "select * from ZONE where ZONE_ID = " + id;
            //query += " ; ";

            query += " ; select * from ZONE_READER_REL where ZONER_ISDELETED=0 and ZONE_ID = " + id;

            var dataSet = _DatabaseHelper.GetDataSet(query, CommandType.Text);

            var dataTableForZone = dataSet.Tables[0];

            Zone zone = new Zone();

            if (dataTableForZone.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTableForZone.Rows)
                {

                    zone.ZONE_ID = Convert.ToInt32(dr["ZONE_ID"]);
                    zone.ZONE_DESCRIPTION = Convert.ToString(dr["ZONE_DESCRIPTION"]);
                    zone.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                }
            }
            var zoneReaders = dataSet.Tables[1];
            List<AcsReaderInfo> acsReaderInfos = new List<AcsReaderInfo>();
            if (zoneReaders.Rows.Count > 0)
            {
                foreach (DataRow dr in zoneReaders.Rows)
                {
                    AcsReaderInfo acsReaderInfo = new AcsReaderInfo();
                    acsReaderInfo.RowId = Convert.ToInt32(dr["READER_ID"]);

                    acsReaderInfos.Add(acsReaderInfo);
                }
            }

            zone.acsReaderInfos = acsReaderInfos;


            return Task.Run(() =>
            {
                return zone;
            });
        }
    }
}