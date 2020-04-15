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
    public interface IZoneReaderRelService
    {
        Task<int> Create(ZoneReaderRel entity, string ipaddress, int activeuser);
        Task<int> Edit(ZoneReaderRel entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
        IQueryable<AcsReaderInfo> GetReadersByZoneId(int id);
    }
    public class ZoneReaderRelService : IZoneReaderRelService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public ZoneReaderRelService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task<int> Create(ZoneReaderRel entity, string ipaddress, int activeuser)
        {
            string query = " Insert into ZONE_READER_REL (ZONE_ID,READER_ID) " +
                          " Values ('" + entity.ZONE_ID + "','" + entity.READER_ID + "') ";
            // query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public Task<int> Edit(ZoneReaderRel entity, string ipaddress, int activeuser)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = " update ZONE_READER_REL set ZONER_ISDELETED = 1, ZONER_DELETEDDATE = '" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "' " +
                        " where ZONER_ISDELETED =0 and ZONE_ID =  " + id;

            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }


        public IQueryable<AcsReaderInfo> GetReadersByZoneId(int id)
        {
            string query = "select READER_ID from zone_reader_rel where ZONER_ISDELETED=0 and ZONE_ID= '" + id + "'";
            List<AcsReaderInfo> acsReaders = new List<AcsReaderInfo>();
            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AcsReaderInfo acsReader = new AcsReaderInfo();
                    acsReader.RowId = Convert.ToInt32(dr["READER_ID"]);                    
                    acsReaders.Add(acsReader);
                }

            }
            return acsReaders.AsQueryable();
        }
    }
}