using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IHolidayService
    {
        Task<int> Create(HolidayDetails entity, string ipaddress, int activeuser);
        Task<int> Edit(HolidayDetails entity, string ipaddress, int activeuser);
        Task<int> Delete(int id, string ipaddress, int activeuser);
        List<HolidayDetails> GetHolidayDetails();
        HolidayDetails GetHolidayDetails(int id);
        int GetTdayCountCreate(DateTime? date, int tday_holiday);
        int GetTdayCountEdit(DateTime? date, int tday_holiday);
        Task<bool> IsUniqHolidayCode(HolidayDetails entity, bool isEdit);

    }

    public class HolidayService : IHolidayService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public HolidayService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task<int> Create(HolidayDetails entity, string ipaddress, int activeuser)
        {
            if(entity.HolidayLoc==null)
            {
                List<HolidayLocation> holidaylist=new List<HolidayLocation>();
                entity.HolidayLoc = holidaylist;
            }
            string ErrorMessage = string.Empty;
            string query = string.Empty;
            int countTady_Holiday_holidaydate = GetTdayCountCreate(entity.HOLIDAY_DATE, 1);
            int countTady_Holiday_swapdate = GetTdayCountCreate(entity.HOLIDAY_SWAP, 1);
            int countHoliday_Holiday_swapdate = GetTdayCountCreate(entity.HOLIDAY_SWAP, 2);
            int countHoliday_Holiday_hoidaydate = GetTdayCountCreate(entity.HOLIDAY_DATE, 2);
            int countHoliday_Holiday_swapdate_swapdate = GetTdayCountCreate(entity.HOLIDAY_DATE, 3);
            if (countTady_Holiday_holidaydate == 1)
            {
                ErrorMessage = "The shift is already created,cannot create holiday for this month";
            }
            if (countTady_Holiday_swapdate == 1)
            {
                ErrorMessage = "Shift is already created,cannot swap holiday date";
            }
            if (countHoliday_Holiday_swapdate == 1)
            {
                ErrorMessage = "Holiday Swap date is already a Holiday";
            }
            if (countHoliday_Holiday_hoidaydate == 1)
            {
                ErrorMessage = "Holiday date already exists";
            }
            if (countHoliday_Holiday_swapdate_swapdate == 1)
            {
                ErrorMessage = "Holiday swap date is already configured to another holiday";
            }
            if(entity.HolidayLoc==null)
            {
                List<HolidayLocation> listholiday = new List<HolidayLocation>();
                entity.HolidayLoc = listholiday;
            }
            if (ErrorMessage == "")
            {
                query = "insert into ent_holiday(holiday_code, holiday_description,holiday_type,holiday_date,holiday_swap, HOLIDAY_CREATEDDATE, holiday_createdby,company_id,is_sync,HOLIDAY_ISDELETED)" +
                    "values('" + entity.HOLIDAY_CODE + "','" + entity.HOLIDAY_DESCRIPTION + "','" + entity.HOLIDAY_TYPE + "','" + entity.HOLIDAY_DATE + "','" + entity.HOLIDAY_SWAP + "','" + DateTime.Now + "','" + entity.ACTIVE_USER + "'," + entity.COMPANY_ID + ",0,0)";
                query += " select @@Identity ;";
                return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
            }
            else
            {
                throw new Exception(ErrorMessage);
            }
            
        }

        public async Task<int> Edit(HolidayDetails entity, string ipaddress, int activeuser)
        {
            string ErrorMessage = string.Empty;
            string query = string.Empty;
            int countTady_Holiday_holidaydate = GetTdayCountEdit(entity.HOLIDAY_DATE, 1);
            int countTady_Holiday_swapdate = GetTdayCountEdit(entity.HOLIDAY_SWAP, 1);
            //int countHoliday_Holiday_swapdate = GetTdayCount(entity.HOLIDAY_SWAP, 2);
            //int countHoliday_Holiday_hoidaydate = GetTdayCount(entity.HOLIDAY_DATE, 2);
            //int countHoliday_Holiday_swapdate_swapdate = GetTdayCount(entity.HOLIDAY_DATE, 3);
            if (countTady_Holiday_holidaydate == 1)
            {
                ErrorMessage = "The shift is already created,cannot Edit holiday for this month";
            }
            if (countTady_Holiday_swapdate == 1)
            {
                ErrorMessage = "Shift is already created,cannot swap holiday date";
            }
            //if (countHoliday_Holiday_swapdate == 1)
            //{
            //    ErrorMessage = "Holiday Swap date is already a Holiday";
            //}
            //if (countholiday_holiday_hoidaydate == 1)
            //{
            //    errormessage = "holiday date already exists";
            //}
            //if (countHoliday_Holiday_swapdate_swapdate == 1)
            //{
            //    ErrorMessage = "Holiday swap date is already configured to another holiday";
            //}
            if (ErrorMessage == "")
            {
                try
                {
                //DateTime? Swap_Date = entity.HOLIDAY_SWAP == null ? null : entity.HOLIDAY_SWAP;
                if (entity.HOLIDAY_SWAP != null)
                {
                    query = "update ENT_HOLIDAY set HOLIDAY_DESCRIPTION='" + entity.HOLIDAY_DESCRIPTION + "' , HOLIDAY_TYPE='" + entity.HOLIDAY_TYPE + "' , HOLIDAY_DATE='" + Convert.ToDateTime(entity.HOLIDAY_DATE) + "' , HOLIDAY_SWAP='" + Convert.ToDateTime(entity.HOLIDAY_SWAP) + "' where holiday_id=" + entity.HOLIDAY_ID + " and isnull(HOLIDAY_ISDELETED,0) = 0";
                }
                else
                {
                    query = "update ENT_HOLIDAY set HOLIDAY_DESCRIPTION='" + entity.HOLIDAY_DESCRIPTION + "' , HOLIDAY_TYPE='" + entity.HOLIDAY_TYPE + "' , HOLIDAY_DATE='" + Convert.ToDateTime(entity.HOLIDAY_DATE) + "' where holiday_id=" + entity.HOLIDAY_ID + " and isnull(HOLIDAY_ISDELETED,0) = 0";        
                }
                
                //query += " select @@Identity ;";
                return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
                }
                catch (Exception ex)
                {
                    
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception(ErrorMessage);
            }
        }

        public async Task<int> Delete(int id, string ipaddress, int activeuser)
        {
            string query = "Update ent_holiday set HOLIDAY_ISDELETED =1 , HOLIDAY_DELETEDDATE ='" + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss") + "' where HOLIDAY_ID = " + id;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public List<HolidayDetails> GetHolidayDetails()
        {
            string query = "select holiday_id, holiday_code, holiday_description,holiday_type,holiday_date,holiday_swap,company_id from ent_holiday where HOLIDAY_ISDELETED =0";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<HolidayDetails> entHolidayList = new List<HolidayDetails>();
            foreach (DataRow dr in x.Rows)
            {
                HolidayDetails entHoliday = new HolidayDetails();
                entHoliday.HOLIDAY_ID = Convert.ToInt32(dr["HOLIDAY_ID"]);
                entHoliday.HOLIDAY_CODE = Convert.ToString(dr["HOLIDAY_CODE"]);
                entHoliday.HOLIDAY_DESCRIPTION = Convert.ToString(dr["HOLIDAY_DESCRIPTION"]);
                entHoliday.HOLIDAY_TYPE = Convert.ToString(dr["HOLIDAY_TYPE"]) == "R" ? "REGIONAL" : Convert.ToString(dr["HOLIDAY_TYPE"]) == "N" ? "NATIONAL" : Convert.ToString(dr["HOLIDAY_TYPE"]) == "O"?"OPTIONAL":"";
                entHoliday.HOLIDAY_DATE = Convert.ToDateTime(dr["HOLIDAY_DATE"]);
                if (Convert.ToDateTime(dr["holiday_swap"]).Year == 1900)
                {
                entHoliday.HOLIDAY_SWAP = null;
                }
                else
                {
                    entHoliday.HOLIDAY_SWAP = Convert.ToDateTime(dr["holiday_swap"]);
                }
                    //DateTime.TryParse("null", out HolidaySwap) ? HolidaySwap : (DateTime?)null;
                entHoliday.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
                entHolidayList.Add(entHoliday);
            }
            return entHolidayList;
        }

        public HolidayDetails GetHolidayDetails(int id)
        {
            string query = "select holiday_id, holiday_code, holiday_description,holiday_type,holiday_date,holiday_swap,company_id from ent_holiday where HOLIDAY_ISDELETED =0 and HOLIDAY_ID =" + id;
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            HolidayDetails entHoliday = new HolidayDetails();
            //DateTime HolidaySwap;
            foreach (DataRow dr in x.Rows)
            {
                //HolidayDetails entHoliday = new HolidayDetails();
                entHoliday.HOLIDAY_ID = Convert.ToInt32(dr["HOLIDAY_ID"]);
                entHoliday.HOLIDAY_CODE = Convert.ToString(dr["HOLIDAY_CODE"]);
                entHoliday.HOLIDAY_DESCRIPTION = Convert.ToString(dr["HOLIDAY_DESCRIPTION"]);
                entHoliday.HOLIDAY_TYPE = Convert.ToString(dr["HOLIDAY_TYPE"]);
                entHoliday.HOLIDAY_DATE = Convert.ToDateTime(dr["HOLIDAY_DATE"]);
                entHoliday.HOLIDAY_SWAP = Convert.ToDateTime(dr["holiday_swap"]);
                entHoliday.COMPANY_ID = Convert.ToInt32(dr["COMPANY_ID"]);
               // entHolidayList.Add(entHoliday);
            }
            return entHoliday;
        }

        public int GetTdayCountCreate(DateTime? date,int tday_holiday)
        {
            string query = string.Empty;
            if (tday_holiday == 1)
            {
               query = "select count(*) from tday where tday_date ='" + date+"'";
            }
            if (tday_holiday == 2)
            {
                query = "select count(*) from ENT_HOLIDAY where HOLIDAY_date ='" + date + "'";
            }
            if (tday_holiday == 3)
            {
                query = "select count(*) from ENT_HOLIDAY where HOLIDAY_SWAP ='" + date + "'";
            }
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            int value=0;
            if (Convert.ToUInt32(x.Rows[0][0]) > 0)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
            return value;
        }
        public int GetTdayCountEdit(DateTime? date, int tday_holiday)
        {
            string query = string.Empty;
            if (tday_holiday == 1)
            {
                query = "select count(*) from tday where tday_date ='" + date + "'";
            }
            //if (tday_holiday == 2)
            //{
            //    query = "select count(*) from ENT_HOLIDAY where HOLIDAY_date ='" + date + "'";
            //}
            //if (tday_holiday == 3)
            //{
            //    query = "select count(*) from ENT_HOLIDAY where HOLIDAY_SWAP ='" + date + "'";
            //}
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            int value = 0;
            if (Convert.ToUInt32(x.Rows[0][0]) > 0)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }
            return value;
        }
        public async Task<bool> IsUniqHolidayCode(HolidayDetails entity, bool isEdit)
        {
            string query = "SELECT count(HOLIDAY_CODE) FROM ENT_HOLIDAY WHERE HOLIDAY_CODE = '" + entity.HOLIDAY_CODE +"' and HOLIDAY_ISDELETED = 0";
            if (isEdit)
                query = query + " and HOLIDAY_ID<>" + entity.HOLIDAY_ID;

            var x = await _DatabaseHelper.GetScalarValue(query, CommandType.Text) == 0 ? false : true;
            return x;
        }
    }
}