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

    public interface IHolidayLocService
    {
        Task<int> Create(List<HolidayLocation> entity, string ipaddress, int activeuser);
        Task<int> Edit(List<HolidayLocation> entity, string ipaddress, int activeuser);
        List<HolidayLocation> GetHolidayLoc();
        List<HolidayLocation> GetHolidayLoc(int id);
        
    }

    public class HolidayLocService : IHolidayLocService
    {

        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public HolidayLocService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public async Task<int> Create(List<HolidayLocation> entity, string ipaddress, int activeuser)
        {
            string query = "delete from ENT_HOLIDAYLOC where HOLIDAY_ID=" + entity[0].HOLIDAY_ID;
            for (int i = 0; i < entity.Count; i++)
            {
                if (entity[i].IS_bool_HOLIDAYLOC_ID != false)
                {
                    query += "  insert into ent_holidayloc(HOLIDAY_ID,IS_OPTIONAL,HOLIDAY_LOC_ID,COMPANY_ID,is_sync)" +
                   "values(" + entity[i].HOLIDAY_ID + "," + entity[i].IS_OPTIONAL + "," + entity[i].HOLIDAY_LOC_ID + "," + entity[i].COMPANY_ID + ",0)";
                }
            }

            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress,activeuser);

        }

        public async Task<int> Edit(List<HolidayLocation> entity, string ipaddress, int activeuser)
        {
            string query = string.Empty;
            query = "delete from ENT_HOLIDAYLOC where HOLIDAY_ID=" + entity[0].HOLIDAY_ID;

            for (int i = 0; i < entity.Count; i++)
            {
                if (entity[i].IS_bool_HOLIDAYLOC_ID != false)
                {
                    query += " insert into ent_holidayloc(HOLIDAY_ID,IS_OPTIONAL,HOLIDAY_LOC_ID,COMPANY_ID,is_sync)" +
                   "values(" + entity[i].HOLIDAY_ID + "," + entity[i].IS_OPTIONAL + "," + entity[i].HOLIDAY_LOC_ID + "," + entity[i].COMPANY_ID + ",0)";
                }
            }
            //query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<HolidayLocation> GetHolidayLoc()
        {
            //string query = "select h.HOLIDAYLOC_ID,h.HOLIDAY_ID, ent.ID, IS_OPTIONAL, HOLIDAY_LOC_ID ,ent.COMPANY_ID  ,OCE_ID ,OCE_DESCRIPTION from [ENT_ORG_COMMON_ENTITIES] ent left join ent_holidayloc h on h.HOLIDAY_LOC_ID = ID where ent.common_types_id =1 and ent.company_id=1";
            string query = "SELECT ID,COMPANY_ID,OCE_ID,OCE_DESCRIPTION from ENT_ORG_COMMON_ENTITIES where common_types_id=1 and company_id=1";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<HolidayLocation> entHolidayList = new List<HolidayLocation>();

            foreach (DataRow dr in x.Rows)
            {
                HolidayLocation entHoliday = new HolidayLocation();
                //entHoliday.HOLIDAYLOC_ID = DataConversion.Int32DataType(dr["HOLIDAYLOC_ID"]);
                //entHoliday.HOLIDAY_ID =  dr["HOLIDAY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HOLIDAY_ID"]);
                //entHoliday.IS_OPTIONAL = DataConversion.Int32DataType(dr["IS_OPTIONAL"]);
                entHoliday.HOLIDAY_LOC_ID = DataConversion.Int32DataType(dr["ID"]);
                entHoliday.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                entHoliday.OCE_ID = Convert.ToString(dr["OCE_ID"]);
                entHoliday.OCE_DESCRIPTION = Convert.ToString(dr["OCE_DESCRIPTION"]);
                entHolidayList.Add(entHoliday);
            }

            return entHolidayList;

        }

        public List<HolidayLocation> GetHolidayLoc(int id)
        {
            string query = "select  h.HOLIDAYLOC_ID , h.HOLIDAY_ID, ent.id, ent.OCE_ID , ent.OCE_DESCRIPTION , h.IS_OPTIONAL ,ent.COMPANY_ID from [ENT_ORG_COMMON_ENTITIES] ent left outer join ent_holidayloc h on ent.id = h.HOLIDAY_LOC_ID and h.HOLIDAY_ID =" + id + " where ent.common_types_id =1";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<HolidayLocation> entHolidayList = new List<HolidayLocation>();

            foreach (DataRow dr in x.Rows)
            {
                HolidayLocation entHoliday = new HolidayLocation();
                entHoliday.HOLIDAYLOC_ID = DataConversion.Int32DataType(dr["HOLIDAYLOC_ID"]);
                entHoliday.HOLIDAY_ID = dr["HOLIDAY_ID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HOLIDAY_ID"]);
                entHoliday.IS_OPTIONAL = DataConversion.Int32DataType(dr["IS_OPTIONAL"]);
                entHoliday.HOLIDAY_LOC_ID = DataConversion.Int32DataType(dr["ID"]);
                entHoliday.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                entHoliday.OCE_ID = Convert.ToString(dr["OCE_ID"]);
                entHoliday.OCE_DESCRIPTION = Convert.ToString(dr["OCE_DESCRIPTION"]);
                entHolidayList.Add(entHoliday);
            }

            return entHolidayList;
        }

    }


}