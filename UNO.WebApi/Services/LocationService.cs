using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{

    public interface ILocationService
    {
        List<Location> Get();

    }
    public class LocationService : ILocationService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public LocationService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }


        public List<Location> Get()
        {

            string query = "select OCE_ID as LOCATION_ID,OCE_DESCRIPTION AS LOCATION_NAME from ENT_ORG_COMMON_ENTITIES where COMMON_TYPES_ID = 1 ";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Location> entLocationList = new List<Location>();

            foreach (DataRow dr in x.Rows)
            {
                Location entLocation = new Location();
                entLocation.LOCATION_ID = Convert.ToString(dr["LOCATION_ID"]);
                entLocation.LOCATION_NAME = Convert.ToString(dr["LOCATION_NAME"]);

                entLocationList.Add(entLocation);
            }
            return entLocationList;
        }


    }
}