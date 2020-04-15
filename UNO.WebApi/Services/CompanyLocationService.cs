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
    public interface ICompanyLocationService
    {
        Task<int> Create(List<CompanyLocationDetails> entity, int id, string ipaddress, int activeuser);
        Task<int> Edit(List<CompanyLocationDetails> entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
        List<CompanyLocationDetails> GetLocation(int id);
    }

    public class CompanyLocationService : ICompanyLocationService
    {
         private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public CompanyLocationService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public async Task<int> Create(List<CompanyLocationDetails> entity, int id, string ipaddress, int activeuser)
        {
            string query = "";
            for (int i = 0; i < entity.Count; i++)
            {
              query = " INSERT INTO ent_company_details (COMPANY_ADDRESS, COMPANY_CITY, COMPANY_PIN, COMPANY_PHONE1, COMPANY_PHONE2, COMPANY_STATE, COMPANY_ID, ADDRESS_TYPE_ID, COMPANY_ISDELETED ) " +
                               " VALUES ('" + entity[i].COMPANY_ADDRESS + "', '" + entity[i].COMPANY_CITY + "', '" + entity[i].COMPANY_PIN + "', '" + entity[i].COMPANY_PHONE1 + "', '" + entity[i].COMPANY_PHONE2 + "', '" + entity[i].COMPANY_STATE + "','" + id + "' ,1,0 ); ";
            }
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress,activeuser);
        }
        public async Task<int> Edit(List<CompanyLocationDetails> entity, string ipaddress, int activeuser)
        {
            //int id;
            string query = "";
           
            for (int i = 0; i < entity.Count; i++)
            {
                 if (entity[i].COMPANY_ADDRESS_ID == 0)
                  {
                      query = " INSERT INTO ent_company_details (COMPANY_ADDRESS, COMPANY_CITY, COMPANY_PIN, COMPANY_PHONE1, COMPANY_PHONE2, COMPANY_STATE, COMPANY_ID, ADDRESS_TYPE_ID, COMPANY_ISDELETED ) " +
                                " VALUES ('" + entity[i].COMPANY_ADDRESS + "', '" + entity[i].COMPANY_CITY + "', '" + entity[i].COMPANY_PIN + "', '" + entity[i].COMPANY_PHONE1 + "', '" + entity[i].COMPANY_PHONE2 + "', '" + entity[i].COMPANY_STATE + "','" + entity[i].COMPANY_ID + "' ,1,0 ); ";
                  }
                 else
                 {
                     query = " UPDATE ent_company_details SET COMPANY_ADDRESS = '" + entity[i].COMPANY_ADDRESS + "', COMPANY_CITY = '" + entity[i].COMPANY_CITY + "', COMPANY_PIN = '" + entity[i].COMPANY_PIN + "', COMPANY_PHONE1 = '" + entity[i].COMPANY_PHONE1 + "', COMPANY_PHONE2 = '" + entity[i].COMPANY_PHONE2 + "', COMPANY_STATE = '" + entity[i].COMPANY_STATE + "' WHERE COMPANY_ADDRESS_ID =  " + entity[i].COMPANY_ADDRESS_ID;
                 }
                 
            }
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = " UPDATE ent_company_details SET COMPANY_ISDELETED = 1  WHERE COMPANY_ID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }
        public List<CompanyLocationDetails> GetLocation(int id)
        {
            string query = "SELECT caddress.COMPANY_ID,caddress.COMPANY_ADDRESS_ID, caddress.COMPANY_ADDRESS, caddress.COMPANY_CITY, caddress.COMPANY_PIN, caddress.COMPANY_PHONE1, caddress.COMPANY_PHONE2," +
                            " caddress.COMPANY_STATE as STATE_CODE, (SELECT VALUE FROM ENT_PARAMS "+
                            " WHERE IDENTIFIER= 'STATES' and MODULE='STATES' and CODE=caddress.COMPANY_STATE) AS COMPANY_STATE,"+
                            " caddress.ADDRESS_TYPE_ID, addresstype.ADDRESS_TYPE  FROM ent_company_details caddress"+
                            " inner join ent_company_address_type addresstype on caddress.ADDRESS_TYPE_ID=addresstype.ADDRESS_TYPE_ID"+
                            " WHERE caddress.COMPANY_ISDELETED=0 and caddress.ADDRESS_TYPE_ID=1 and caddress.COMPANY_ID ="+id;
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<CompanyLocationDetails> entLocationsList = new List<CompanyLocationDetails>();

            foreach (DataRow dr in x.Rows)
            {
                CompanyLocationDetails CompanyLocationsobj = new CompanyLocationDetails();
                CompanyLocationsobj.COMPANY_ID = Convert.ToInt16(dr["COMPANY_ID"]);
                CompanyLocationsobj.COMPANY_ADDRESS_ID = Convert.ToInt32(dr["COMPANY_ADDRESS_ID"]);
                CompanyLocationsobj.COMPANY_ADDRESS = Convert.ToString(dr["COMPANY_ADDRESS"]);
                CompanyLocationsobj.COMPANY_CITY = Convert.ToString(dr["COMPANY_CITY"]);
                CompanyLocationsobj.COMPANY_PIN = Convert.ToString(dr["COMPANY_PIN"]);
                CompanyLocationsobj.COMPANY_PHONE1 = Convert.ToString(dr["COMPANY_PHONE1"]);
                CompanyLocationsobj.COMPANY_PHONE2 = Convert.ToString(dr["COMPANY_PHONE2"]);
                CompanyLocationsobj.COMPANY_STATE = Convert.ToString(dr["COMPANY_STATE"]);
                CompanyLocationsobj.ADDRESS_TYPE = Convert.ToString(dr["ADDRESS_TYPE"]);
                CompanyLocationsobj.ADDRESS_TYPE_ID = Convert.ToInt32(dr["ADDRESS_TYPE_ID"]);
                CompanyLocationsobj.STATE_CODE = Convert.ToString(dr["STATE_CODE"]);
                entLocationsList.Add(CompanyLocationsobj);
            }
            return entLocationsList;
        }
    }
}