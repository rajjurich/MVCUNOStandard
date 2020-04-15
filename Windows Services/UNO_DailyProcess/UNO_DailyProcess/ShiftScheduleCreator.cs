using Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNO.DAL;

namespace UNO_DailyProcess
{
    public class ShiftScheduleCreator
    {
        private ILog _log;
        private DatabaseHelper _DatabaseHelper;
        private UnitOfWork _unitOfWork;
        public ShiftScheduleCreator()
        {
            _log = Log.GetInstance;
            _unitOfWork = new UnitOfWork();
            _DatabaseHelper = new DatabaseHelper(_unitOfWork);
        }
        public void StartScheduleCreating()
        {

            while (true)
            {
                createShiftForMonth().Wait();
            }
        }

        private async Task createShiftForMonth()
        {
            //_log.WriteLog("shift", LogType.Audit);
            var forMonth = ConfigurationManager.AppSettings["ForMonth"] == null || ConfigurationManager.AppSettings["ForMonth"].ToString() == string.Empty ? 0 : Convert.ToInt32(ConfigurationManager.AppSettings["ForMonth"]);

            int getMonth = DateTime.Now.AddMonths(forMonth).Month;
            int getYear = DateTime.Now.AddMonths(forMonth).Year;
            int NumberOfDaysInCurrentMonth = DateTime.DaysInMonth(getYear, getMonth);
            int ShiftStartDay = 1;



            string getPreviousDayShift = " (select top 1 TDAY_SFTREPO from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td order by tday_date desc) ";

            string getShiftOrderOfPreviousDayShift = "(select shiftorder from shift_shift_pattern where SHIFT_ID =  " + getPreviousDayShift + " )";


            string getShiftsCountInPattern = " (select count(1) from shift_shift_pattern where SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE) ";

            string getRowNumber = " row_number() over(partition by EMPLOYEE_ID order by EMPLOYEE_ID ) ";


            string getShiftOrder = " case when " + getShiftOrderOfPreviousDayShift + " = " + getShiftsCountInPattern + "  or " + getShiftOrderOfPreviousDayShift + "  is null then 0 else " + getShiftOrderOfPreviousDayShift + " end";

            string getShiftOrder1 = " case when " + getShiftOrderOfPreviousDayShift + " = " + getShiftsCountInPattern + "  or " + getShiftOrderOfPreviousDayShift + "  is null then 1 else " + getShiftOrderOfPreviousDayShift + " end";

            string getDaysCountOfPreviousShift = "(select ct from (select  count(1) as ct from tday where tday_empcde=tna.ETC_EMP_ID and tday_date<tday_days.td and tday_sftrepo = " + getPreviousDayShift + ")t)";


            StringBuilder sb = new StringBuilder();
            sb.Append(" insert into tday(TDAY_EMPCDE,TDAY_DATE,TDAY_SFTASSG,TDAY_SFTREPO,TDAY_STATUS_ID) ");
            sb.AppendLine("  select EMPLOYEE_ID,td,shiftrepo,shiftrepo,tday_status from ( ");
            sb.AppendLine(" select EMPLOYEE_ID,td ");
            sb.AppendLine(",case ");
            sb.AppendLine("when SHIFT_PATTERN_TYPE='MN' then monthly_shift ");
            sb.AppendLine("when SHIFT_PATTERN_TYPE='DL' then daily_shift ");
            sb.AppendLine("when SHIFT_PATTERN_TYPE='WK' then weekly_shift ");
            sb.AppendLine("when SHIFT_PATTERN_TYPE='BW' then biweekly_shift ");
            sb.AppendLine("end as shiftrepo ");
            sb.AppendLine(",tday_status ");
            sb.AppendLine("from ");
            sb.AppendLine("( ");
            sb.AppendLine(" select *");
            sb.AppendLine(",(select top 1 shift_id from shift_shift_pattern where SHIFT_PATTERN_ID= shiftpatternid and shiftorder = daily_shiftorder) as daily_shift ");
            sb.AppendLine(",(select top 1 shift_id from shift_shift_pattern where SHIFT_PATTERN_ID= shiftpatternid and shiftorder = weekly_shiftorder) as weekly_shift ");
            sb.AppendLine(",(select top 1 shift_id from shift_shift_pattern where SHIFT_PATTERN_ID= shiftpatternid and shiftorder = biweekly_shiftorder) as biweekly_shift ");
            sb.AppendLine("from ( ");
            sb.AppendLine(" select EMPLOYEE_ID ");
            sb.AppendLine(" ,td");
            sb.AppendLine(" ,SHIFT_PATTERN_ID as shiftpatternid ");
            sb.AppendLine(" ,SHIFT_PATTERN_TYPE ");
            sb.AppendLine(" ," + getPreviousDayShift + " as previous_shift ");


            sb.AppendLine(",(select top 1 SHIFT_ID from shift_shift_pattern where  SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE and shiftorder > " + getShiftOrder + " order by shiftorder asc ) as monthly_shift");


            sb.AppendLine(", case when " + getRowNumber + " <= (");
            sb.Append(" 7 - " + getDaysCountOfPreviousShift + " ) ");
            sb.AppendLine(" then " + getShiftOrder1 + "");

            sb.AppendLine(" else ");

            sb.AppendLine(" case when (" + getRowNumber + " - (7  -  " + getDaysCountOfPreviousShift + " ) - 1)/7 + " + getShiftOrder1 + "+1 > " + getShiftsCountInPattern + "");
            sb.AppendLine(" then ");
            sb.Append(" ( " + getRowNumber + " - (7  -  " + getDaysCountOfPreviousShift + ") - 1 )/7 + " + getShiftOrder1 + "+1 - " + getShiftsCountInPattern + "");
            sb.AppendLine(" else ");
            sb.AppendLine(" ( " + getRowNumber + " - (7  -  " + getDaysCountOfPreviousShift + "  )- 1)/7 + " + getShiftOrder1 + "+1 ");
            sb.AppendLine(" end ");


            sb.AppendLine(" end   as  weekly_shiftorder");


            sb.AppendLine(", case when " + getRowNumber + " <= (");
            sb.Append(" 14 - " + getDaysCountOfPreviousShift + " ) ");
            sb.AppendLine(" then " + getShiftOrder1 + "");

            sb.AppendLine(" else ");

            sb.AppendLine(" case when (" + getRowNumber + " - (14  -  " + getDaysCountOfPreviousShift + " ) - 1)/14 + " + getShiftOrder1 + "+1 > " + getShiftsCountInPattern + "");
            sb.AppendLine(" then ");
            sb.Append(" ( " + getRowNumber + " - (14  -  " + getDaysCountOfPreviousShift + ") - 1 )/14 + " + getShiftOrder1 + "+1 - " + getShiftsCountInPattern + "");
            sb.AppendLine(" else ");
            sb.AppendLine(" ( " + getRowNumber + " - (14  -  " + getDaysCountOfPreviousShift + "  )- 1)/14 + " + getShiftOrder1 + "+1 ");
            sb.AppendLine(" end ");


            sb.AppendLine(" end   as  biweekly_shiftorder");

            sb.AppendLine(", case when (" + getRowNumber + "% " + getShiftsCountInPattern + "+ " + getShiftOrder + ")>" + getShiftsCountInPattern + " ");

            sb.AppendLine(" then ");
            sb.Append(" (" + getRowNumber + "% " + getShiftsCountInPattern + "+ " + getShiftOrder + ") - " + getShiftsCountInPattern + "");
            sb.AppendLine(" when (" + getRowNumber + "% " + getShiftsCountInPattern + "+ " + getShiftOrder + ")< 1 ");
            sb.AppendLine(" then " + getShiftsCountInPattern + "");
            sb.AppendLine(" else ");
            sb.Append(" (" + getRowNumber + "% " + getShiftsCountInPattern + "+ " + getShiftOrder + ")");
            sb.AppendLine(" end as daily_shiftorder");

            sb.AppendLine(" ,case ");
            sb.Append(" when wkend.MWK_PAT like '%'+cast(floor((day(td)-1)/7)+1 as varchar(2))+'%' and wkend.MWK_DAY=datepart(dw,td) ");
            sb.AppendLine(" then (select top 1 TDAY_STATUS_ID from tday_status where TDAY_STATUS_CODE='ABW2') ");
            sb.AppendLine(" when wkoff.MWK_PAT like '%'+cast(floor((day(td)-1)/7)+1 as varchar(2))+'%' and wkoff.MWK_DAY=datepart(dw,td) ");
            sb.AppendLine(" then (select top 1 TDAY_STATUS_ID from tday_status where TDAY_STATUS_CODE='ABWO') ");
            sb.AppendLine(" else (select top 1 TDAY_STATUS_ID from tday_status where TDAY_STATUS_CODE='AB') ");
            sb.AppendLine(" end as tday_status ");

            sb.AppendLine(" from ENT_EMPLOYEE_DTLS eod ");
            sb.AppendLine(" cross join (select '" + @getYear + "'+'/'+'" + @getMonth + "'+'/'+  tdays as td,tday  from tday_days where tday<" + NumberOfDaysInCurrentMonth + "+1) as tday_days ");
            sb.AppendLine(" join TNA_EMPLOYEE_TA_CONFIG tna on tna.ETC_EMP_ID=eod.EMPLOYEE_ID ");
            sb.AppendLine(" join ta_shift_pattern pat on pat.SHIFT_PATTERN_ID=tna.ETC_SHIFTCODE ");
            sb.AppendLine(" join TA_WKLYOFF wkend on wkend.MWK_CD=tna.ETC_WEEKEND ");
            sb.AppendLine(" join TA_WKLYOFF wkoff on wkoff.MWK_CD=tna.ETC_WEEKOFF ");
            sb.AppendLine(" where EOD_STATUS=1 and eod.EMPLOYEE_ID not in ");
            sb.AppendLine(" (select TDAY_EMPCDE from tday where month(tday_date)='" + getMonth + "' and year(tday_date)='" + getYear + "' and day(tday_date)>" + ShiftStartDay + "-1) ");
            sb.AppendLine(" and td>=EOD_JOINING_DATE and day(td)>" + ShiftStartDay + "-1 ");

            sb.AppendLine(" )a ");
            sb.AppendLine(" )b ");
            sb.AppendLine(" )c ");

            sb.AppendLine(" select @@ROWCOUNT as rc");
            string query = sb.ToString();

            int x = await _DatabaseHelper.Insert(query, CommandType.Text,"ShiftScheduleCreator.cs",0);

            if (x > 0)
            {
                _log.WriteLog(string.Format("Shift Schedule Creation done for Month {0} and Year {1}, no. of records added {2}", getMonth, getYear, x), LogType.Audit);
            }

        }
    }
}
