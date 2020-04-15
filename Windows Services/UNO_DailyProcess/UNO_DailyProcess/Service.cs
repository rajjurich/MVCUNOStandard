using Logger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UNO.DAL;

namespace UNO_DailyProcess
{
    public partial class Service : ServiceBase
    {
        Thread dailyProcessThread;
        Thread shiftCreatorThread;
        private ILog _log;
        private DatabaseHelper _DatabaseHelper;
        private UnitOfWork _unitOfWork;
        

        public Service()
        {
            InitializeComponent();
            _log = Log.GetInstance;
            _unitOfWork = new UnitOfWork();
            _DatabaseHelper = new DatabaseHelper(_unitOfWork);
        }
        public void OnDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            ShiftScheduleCreator objListener = new ShiftScheduleCreator();
            shiftCreatorThread = new System.Threading.Thread(objListener.StartScheduleCreating);
            shiftCreatorThread.Start();
            dailyProcessThread = new Thread(StartDailyProcess);
            //dailyProcessThread.IsBackground = true;
            dailyProcessThread.Start();

        }
        protected override void OnStop()
        {
            dailyProcessThread.Abort();
            shiftCreatorThread.Abort();

        }

        Dictionary<string, Thread> RunningThreads = new Dictionary<string, Thread>();
        public void StartDailyProcess()
        {
            while (true)
            {
                //_log.WriteLog(".", LogType.Audit);
                try
                {
                    //int count = InsertTascData().Result;
                    //if (count > 0)
                    //{
                        ExceuteDailyProcess().Wait();
                    //}
                }
                catch (Exception ex)
                {
                    _log.ExceptionsCaught(ex, "Service.StartDailyProcess");

                }

            }


        }

        private async Task<int> InsertTascData()
        {
            //_log.WriteLog("check tasc",LogType.Audit);
            string query = GetTascDataQuery();
            int x = await _DatabaseHelper.Insert(query, CommandType.Text,"Service.cs",0);

            if (x > 0)
            {
                _log.WriteLog(string.Format("Insert in Tasc Successfully done, Total no. of records inserted in Tasc {0}", x), LogType.Audit);
            }

            return x;
        }

        private async Task ExceuteDailyProcess()
        {
            //_log.WriteLog("exec daily process", LogType.Audit);
            string query = GetDailyProcessQuery();
            int x = await _DatabaseHelper.Insert(query, CommandType.Text, "Service.cs", 0);

            if (x > 0)
            {
                _log.WriteLog(string.Format("Daily Process Successfully Executed, Total no. of punch records processed {0}", x), LogType.Audit);
            }
        }

        private string GetTascDataQuery()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" set nocount on ");
            sb.AppendLine(" SELECT rtrim(ltrim(Event_Employee_Code))as Event_Employee_Code ");
            sb.AppendLine(" ,Event_Datetime AS EVENT_TIME ");
            sb.AppendLine(" ,cast(cast(Event_Datetime as date) as datetime) As EVENT_DATE ");
            sb.AppendLine(" ,event_mode ");
            sb.AppendLine(" ,Event_Trace ");
            sb.AppendLine(" ,Event_ID ");
            sb.AppendLine(" ,COMPANY_ID ");
            sb.AppendLine(" INTO #TEMP ");
            sb.AppendLine(" FROM [UNOMVC_TRANSACTION].dbo.ACS_EVENTS WHERE TASC_FLAG=0 ");

            sb.AppendLine(" INSERT INTO [UNOMVC_TRANSACTION].dbo.TASC ");
            sb.AppendLine(" (TAsc_empcode,TAsc_time,TAsc_date,TAsc_Mode,Tasc_flag,Event_Trace,Event_ID,COMPANY_ID) ");
            sb.AppendLine(" SELECT Event_Employee_Code,EVENT_TIME,EVENT_DATE,event_mode,0,Event_Trace,Event_ID,COMPANY_ID  FROM #TEMP ");


            sb.AppendLine(" UPDATE [UNOMVC_TRANSACTION].dbo.ACS_EVENTS ");
            sb.AppendLine(" SET TASC_FLAG=1 ");
            sb.AppendLine(" WHERE Event_ID IN (SELECT Event_ID FROM #TEMP) ");

            sb.AppendLine(" DROP TABLE #TEMP ");

            sb.AppendLine(" select @@rowcount ");
            return sb.ToString();

        }
        private string GetDailyProcessQuery()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" set nocount on ");
            sb.AppendLine(" declare @tmptbl Table(TAsc_empcode varchar(20),TAsc_Mode varchar(5),TAsc_time datetime,tascdate datetime,startmin datetime,endmax datetime,COMPANY_ID int) ");
            sb.AppendLine(" insert into @tmptbl(TAsc_empcode,TAsc_Mode,TAsc_time,tascdate,startmin,endmax,COMPANY_ID) ");
            sb.AppendLine(" select TAsc_empcode,TAsc_Mode,TAsc_time,tascdate,startmin,endmax,COMPANY_ID from ");
            sb.AppendLine(" ( ");
            sb.AppendLine(" select TAsc_empcode ");
            sb.AppendLine(" ,case ");
            sb.AppendLine(" when TAsc_time between startminboundary and endmaxboundary then cast(cast(startminboundary as date) as datetime) ");
            sb.AppendLine(" when TAsc_time between startminboundaryprev and endmaxboundaryprev then cast(cast(startminboundaryprev as date) as datetime) ");
            sb.AppendLine(" when TAsc_time between startminboundarynext and endmaxboundarynext then cast(cast(startminboundarynext as date) as datetime) ");
            sb.AppendLine(" end as tascdate ");

            sb.AppendLine(" ,cast(TAsc_time as time) TAsctime ");
            sb.AppendLine(" ,TAsc_time ");
            sb.AppendLine(" ,TAsc_Mode ");

            sb.AppendLine(" ,case ");
            sb.AppendLine(" when TAsc_time between startminboundary and endmaxboundary then startmin ");
            sb.AppendLine(" when TAsc_time between startminboundaryprev and endmaxboundaryprev then startminprev ");
            sb.AppendLine(" when TAsc_time between startminboundarynext and endmaxboundarynext then startminnext ");
            sb.AppendLine(" end as startmin ");
            sb.AppendLine(" ,case ");
            sb.AppendLine(" when TAsc_time between startminboundary and endmaxboundary then endmax ");
            sb.AppendLine(" when TAsc_time between startminboundaryprev and endmaxboundaryprev then endmaxprev ");
            sb.AppendLine(" when TAsc_time between startminboundarynext and endmaxboundarynext then endmaxnext ");
            sb.AppendLine(" end as endmax,tasc.COMPANY_ID ");
            sb.AppendLine(" from ");
            sb.AppendLine(" ( ");
            sb.AppendLine(" select TAsc_empcode,TAsc_date,TAsc_time,EOD_COMPANY_ID,tday_sftrepo,shift_id,TAsc_Mode ");
            sb.AppendLine(" ,datepart(dw,TAsc_date) as dd,cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2)) as wd ");
            sb.AppendLine(" ,wkend.mwk_pat as wepat ");
            sb.AppendLine(" ,wkend.mwk_day as weday ");
            sb.AppendLine(" ,TAsc_date + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START ");
            sb.AppendLine(" else SHIFT_START end - SHIFT_EARLY_SEARCH_HRS as startminboundary ");
            sb.AppendLine(" ,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END ");
            sb.AppendLine(" else SHIFT_END end + SHIFT_LATE_SEARCH_HRS as endmaxboundary ");
            sb.AppendLine(" ,TAsc_date -1  + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START ");

            sb.AppendLine(" else SHIFT_START end - SHIFT_EARLY_SEARCH_HRS as startminboundaryprev ");
            sb.AppendLine(" ,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end -1 + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END ");
            sb.AppendLine(" else SHIFT_END end + SHIFT_LATE_SEARCH_HRS as endmaxboundaryprev ");
            sb.AppendLine(" ,TAsc_date +1  + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START ");
            sb.AppendLine(" else SHIFT_START end - SHIFT_EARLY_SEARCH_HRS as startminboundarynext ");
            sb.AppendLine(" ,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end+1 + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END ");
            sb.AppendLine(" else SHIFT_END end + SHIFT_LATE_SEARCH_HRS as endmaxboundarynext ");
            sb.AppendLine(" ,TAsc_date + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START ");
            sb.AppendLine(" else SHIFT_START end as startmin ");
            sb.AppendLine(" ,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END ");
            sb.AppendLine(" else SHIFT_END end as endmax ");
            sb.AppendLine(" ,TAsc_date -1  + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START ");
            sb.AppendLine(" else SHIFT_START end as startminprev ");
            sb.AppendLine(" ,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end -1 + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END ");
            sb.AppendLine(" else SHIFT_END end as endmaxprev ");
            sb.AppendLine(" ,TAsc_date +1  + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_START ");
            sb.AppendLine(" else SHIFT_START end as startminnext ");
            sb.AppendLine(" ,TAsc_date + case when SHIFT_START > SHIFT_END then 1 else 0 end+1 + case ");
            sb.AppendLine(" when SHIFT_WEEKEND_DIFF_TIME=1 and  wkend.mwk_pat  not like '%'+cast(floor((day(TAsc_date)-1)/7)+1 as varchar(2))+'%' and wkend.mwk_day=datepart(dw,TAsc_date)	then SHIFT_WEEKEND_END ");
            sb.AppendLine(" else SHIFT_END end  as endmaxnext ,tasc.COMPANY_ID ");

            sb.AppendLine(" from UNOMVC_TRANSACTION.dbo.tasc ");
            sb.AppendLine(" join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EOD_EMPID=tasc.TAsc_empcode ");
            sb.AppendLine(" join unomvc.dbo.tday tday on tday.TDAY_DATE=TASC.TAsc_date and tday.TDAY_EMPCDE=e.EMPLOYEE_ID  ");
            sb.AppendLine(" join unomvc.[dbo].[TNA_EMPLOYEE_TA_CONFIG] tna on tna.etc_emp_id=e.EMPLOYEE_ID ");

            sb.AppendLine(" join (select * from unomvc.dbo.TA_WKLYOFF  where MWK_OFF=0 and IsDeleted=0) wkoff on TNA.ETC_WEEKOFF=wkoff.MWK_CD and TNA.ETC_ISDELETED=0 ");
            sb.AppendLine(" join (select * from unomvc.dbo.TA_WKLYOFF  where MWK_OFF=1 and IsDeleted=0) wkend on TNA.ETC_WEEKEND=wkend.MWK_CD and TNA.ETC_ISDELETED=0 ");
            sb.AppendLine(" join unomvc.dbo.ta_shift tashift on tashift.COMPANY_ID=e.EOD_COMPANY_ID ");
            sb.AppendLine(" and tashift.SHIFT_ID=tday.tday_sftrepo ");

            sb.AppendLine(" WHERE	1=1 and Tasc_flag=0 ");
            sb.AppendLine(" ) tasc ");

            sb.AppendLine(" )b where tascdate is not null ");

            sb.AppendLine(" order by tascdate,tasc_mode ");





            sb.AppendLine(" --updates--");
            sb.AppendLine(" update t ");
            sb.AppendLine(" set t.TDAY_INTIME=intime,t.TDAY_InDATE=intime,t.TDAY_LATE=case when intime > (startmin + LATE_COMING) then intime - (startmin + LATE_COMING) else t.TDAY_LATE end ");

            sb.AppendLine(" ,t.TDAY_EROT=case when EXTRA_CHECK=1 ");
            sb.AppendLine(" then case when intime < startmin then  startmin-intime else t.TDAY_EROT end ");
            sb.AppendLine(" else t.TDAY_EROT end ");
            sb.AppendLine(" from unomvc.dbo.tday  t ");
            sb.AppendLine(" join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE ");
            sb.AppendLine(" join( ");
            sb.AppendLine(" select TAsc_empcode,tascdate,min(tasc_time) as intime,startmin,COMPANY_ID from @tmptbl ");
            sb.AppendLine(" where tasc_mode in ('i','n') ");
            sb.AppendLine(" group by tascdate,TAsc_empcode,startmin,COMPANY_ID)c ");
            sb.AppendLine(" on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null ");
            sb.AppendLine(" join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 ");

            sb.AppendLine(" update t ");
            sb.AppendLine(" set t.TDAY_OUTIME=case when outtime=t.TDAY_INTIME then null else outtime end,t.TDAY_OUDATE=case when outtime=t.TDAY_INTIME then null else outtime end,t.TDAY_EARLY=case when outtime=t.TDAY_INTIME then null else case when outtime < (endmax-EARLY_GOING) then (endmax-EARLY_GOING) -outtime  else t.TDAY_EARLY end end ");
            sb.AppendLine(" ,t.TDAY_LTOT=case when outtime=t.TDAY_INTIME then null else case when EXTRA_CHECK=1 ");
            sb.AppendLine(" then case when outtime > endmax  then  outtime-endmax else t.TDAY_LTOT end ");
            sb.AppendLine(" else t.TDAY_LTOT end end ");
            sb.AppendLine(" from unomvc.dbo.tday  t ");
            sb.AppendLine(" join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE ");
            sb.AppendLine(" join( ");
            sb.AppendLine(" select TAsc_empcode,tascdate,max(tasc_time) as outtime,endmax ,COMPANY_ID from @tmptbl ");
            sb.AppendLine(" where tasc_mode in ('o','n') ");
            sb.AppendLine(" group by tascdate,TAsc_empcode,endmax,COMPANY_ID)c on c.TAsc_empcode=e.EOD_EMPID  and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null ");
            sb.AppendLine(" join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 ");




            sb.AppendLine(" update t ");

            sb.AppendLine(" set TDAY_LTOT = case when EXTRA_CHECK=1 ");
            sb.AppendLine(" then case when TDAY_LTOT > EXHRS_AFTER_SHIFT_HRS then TDAY_LTOT else null end ");
            sb.AppendLine(" else ");
            sb.AppendLine(" TDAY_LTOT ");
            sb.AppendLine(" end ");
            sb.AppendLine(" , TDAY_EROT = case when EXTRA_CHECK=1 ");
            sb.AppendLine(" 	  then case when TDAY_EROT > EXHRS_BEFORE_SHIFT_HRS then TDAY_EROT else null end  ");
            sb.AppendLine(" else ");
            sb.AppendLine(" TDAY_EROT ");
            sb.AppendLine(" end ");


            sb.AppendLine(" from unomvc.dbo.tday  t ");
            sb.AppendLine(" join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE ");
            sb.AppendLine(" join( ");
            sb.AppendLine(" select distinct TAsc_empcode,tascdate,COMPANY_ID from @tmptbl ");

            sb.AppendLine(" )c on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null ");
            sb.AppendLine(" join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 ");


            sb.AppendLine(" update t ");
            sb.AppendLine(" set TDAY_EXHR =isnull(case when DED_FROM_EXHRS_LATE_COMING=1 and TDAY_LATE<TDAY_LTOT then TDAY_LTOT-TDAY_LATE else TDAY_LTOT end,'1900-01-01') + ");
            sb.AppendLine(" isnull(case when DED_FROM_EXHRS_EARLY_GOING=1 and TDAY_EARLY<TDAY_EROT then TDAY_EROT-TDAY_EARLY else TDAY_EROT end,'1900-01-01') ");
            sb.AppendLine(" ,TDAY_WRKHR=TDAY_OUTIME-TDAY_INTIME ");
            sb.AppendLine(" from unomvc.dbo.tday  t ");
            sb.AppendLine(" join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE ");
            sb.AppendLine(" join( ");
            sb.AppendLine(" select distinct TAsc_empcode,tascdate,COMPANY_ID from @tmptbl ");

            sb.AppendLine(" )c on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null ");
            sb.AppendLine(" join unomvc.[dbo].ENT_CATEGORY cat on cat.ORG_CATEGORY_ID=e.EOD_CATEGORY_ID and ISDELETED=0 ");


            sb.AppendLine(" update t ");
            sb.AppendLine(" set t.TDAY_STATUS_ID = case when t.TDAY_INTIME is not null and t.TDAY_OUTIME is not null then");
            sb.AppendLine(" case when st.tday_status_code in ('AB','PRAB','PR','ABWO','ABW2','ABHO','PRWO','PRW2','PRHO','LWP') then ");
            sb.AppendLine(" case ");
            sb.AppendLine(" when TDAY_WRKHR > (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='fd') then (select top 1 TDAY_STATUS_ID from unomvc.dbo.[TDAY_STATUS] where TDAY_STATUS_CODE='pr' + isnull((SUBSTRING(st.Tday_status_code,3,2)),'')) ");
            sb.AppendLine(" when TDAY_WRKHR < (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='fd') and TDAY_WRKHR > (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='hd') then (select top 1 TDAY_STATUS_ID from unomvc.dbo.[TDAY_STATUS] where TDAY_STATUS_CODE='pr' + isnull((SUBSTRING(st.Tday_status_code,3,2)),'ab')) ");
            sb.AppendLine(" when TDAY_WRKHR < (select cast(value as datetime) from unomvc.dbo.ent_params where module='ta' and identifier='DAILYPROCESS' and code='hd') then (select top 1 TDAY_STATUS_ID from unomvc.dbo.[TDAY_STATUS] where TDAY_STATUS_CODE='ab'+ isnull((SUBSTRING(st.Tday_status_code,3,2)),'')) ");
            sb.AppendLine(" else t.TDAY_STATUS_ID end ");
            sb.AppendLine(" else t.TDAY_STATUS_ID ");
            sb.AppendLine(" end else t.TDAY_STATUS_ID end ");


            sb.AppendLine(" from unomvc.dbo.tday  t ");
            sb.AppendLine(" join unomvc.dbo.ENT_EMPLOYEE_DTLS e on e.EMPLOYEE_ID=t.TDAY_EMPCDE ");
            sb.AppendLine(" join( ");
            sb.AppendLine(" select distinct TAsc_empcode,tascdate,COMPANY_ID from @tmptbl ");

            sb.AppendLine(" )c on c.TAsc_empcode=e.EOD_EMPID and e.EOD_COMPANY_ID=c.[COMPANY_ID] and c.tascdate=t.TDAY_DATE and tascdate is not null ");
            sb.AppendLine(" join unomvc.[dbo].[TDAY_STATUS] st on st.TDAY_STATUS_ID=t.TDAY_STATUS_ID ");




            sb.AppendLine(" update UNOMVC_TRANSACTION.dbo.tasc set tasc_flag=1 where tasc_flag=0 ");

            sb.AppendLine(" select @@rowcount ");







            return sb.ToString();
        }


    }
}
