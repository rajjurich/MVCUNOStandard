using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{

    public interface ITDayService
    {
        IQueryable<TDayDto> GetSingle(int id);

    }

    public class TDayService : ITDayService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public TDayService(IUnitOfWork unitOfWork
            , DatabaseHelper databaseHelper
            )
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;

        }

        public IQueryable<TDayDto> GetSingle(int id)
        {
            string query = string.Empty;

            query = " select * from tday td join TDAY_STATUS ts on ts.TDAY_STATUS_ID=td.TDAY_STATUS_ID where tday_empcde in (select top 1 Employee_id from ent_user usr join ENT_EMPLOYEE_DTLS epd on epd.EOD_EMPID=usr.USER_CODE and user_id=" + id + ") ";


            var dataTable = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<TDayDto> tDayDtos = new List<TDayDto>();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    TDayDto tDayDto = new TDayDto();
                    tDayDto.TDAY_ID = Convert.ToInt32(dr["TDAY_ID"]);
                    tDayDto.TDAY_EMPCDE = Convert.ToInt32(dr["TDAY_EMPCDE"]);
                    tDayDto.TDAY_DATE = Convert.ToDateTime(dr["TDAY_DATE"]);
                    tDayDto.Tday_Status = Convert.ToString(dr["TDAY_STATUS"]);
                    tDayDto.Tday_Status = Convert.ToString(dr["TDAY_STATUS"]);
                    tDayDto.InTime = dr["TDAY_INTIME"] == DBNull.Value ? "" : Convert.ToDateTime(dr["TDAY_INTIME"]).ToString("HH:mm");
                    tDayDto.OutTime = dr["TDAY_OUTIME"] == DBNull.Value ? "" : Convert.ToDateTime(dr["TDAY_OUTIME"]).ToString("HH:mm");
                    var wkday = Convert.ToString(dr["TDAY_STATUS_CODE"]);
                    if (wkday.Contains("W2"))
                    {
                        wkday = "Weekend";
                    }
                    else if (wkday.Contains("WO"))
                    {
                        wkday = "Weekoff";
                    }
                    else if (wkday.Contains("HO"))
                    {
                        wkday = "Holiday";
                    }
                    else
                    {
                        wkday = "Workday";
                    }

                    tDayDto.Workday = wkday;
                    tDayDto.ClassName = Convert.ToString(dr["ClassName"]);
                    tDayDtos.Add(tDayDto);
                }
            }

            return tDayDtos.AsQueryable();
        }
    }





}