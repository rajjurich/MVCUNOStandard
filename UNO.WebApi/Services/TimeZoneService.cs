using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface ITimeZoneService
    {
        IQueryable<AcsTimeZone> Get();
    }
    public class TimeZoneService : ITimeZoneService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public TimeZoneService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
           )
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
           
        }
        public IQueryable<AcsTimeZone> Get()
        {
            string query = string.Empty;
            query = "select * from ACS_TIMEZONE where TZ_ISDELETED=0";

            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<AcsTimeZone> acsTimeZoneList = new List<AcsTimeZone>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    AcsTimeZone acsTimeZone = new AcsTimeZone();

                    acsTimeZone.TZ_ID = Convert.ToInt64(dr["TZ_ID"]);                    
                    acsTimeZone.TZ_DESCRIPTION = Convert.ToString(dr["TZ_DESCRIPTION"]);                    
                    acsTimeZone.TZ_ISDELETED = Convert.ToBoolean(dr["TZ_ISDELETED"]);
                    
                    acsTimeZoneList.Add(acsTimeZone);
                }
            }

            return acsTimeZoneList.AsQueryable();
        }
    }
}