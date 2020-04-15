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
    public interface IEventBrowserService
    {
        List<EventBrowserDetails> GetEvents(int Event_Type, int Level);
    }


    public class EventBrowserService : IEventBrowserService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public EventBrowserService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public List<EventBrowserDetails> GetEvents(int Event_Type, int Level)
        {
            string query = "select  distinct AE.Event_ID,CASE AE.Event_Type  WHEN '01' THEN 'CARD' WHEN '02' THEN 'ALARM' WHEN '03' THEN 'REX' ELSE NULL END AS Event_Type,AE.Event_Datetime ," +
                            "Case when AE.Event_Employee_Code=NULL then ''" +
                            "else ( select EMP.EPD_FIRST_NAME+' '+EMP.EPD_LAST_NAME from ENT_EMPLOYEE_DTLS EMP where (EMP.EOD_EMPID) = AE.Event_Employee_Code) End as Empname," +
                            "Case when AE.Event_Employee_Code=NULL then NULL " +
                            "else AE.Event_Employee_Code END as Event_Employee_Code," +
                            "Case  When AE.Event_Reader_ID=NULL then NULL " +
                            "else ( Select READER_DESCRIPTION from ACS_READER " +
                            "with(nolock) where READER_ID=AE.Event_Reader_ID and CTLR_ID=AE.Event_Controller_ID)" +
                            "END as ReaderD, Case When AE.Event_Controller_ID=NULL then NULL else  CTLR_DESCRIPTION  end  as CtrlD,'Access Granted' as Event_Status  ," +
                            "CASE AE.Event_Alarm_Type  WHEN '1' THEN 'Door not Opened on Valid Swipe' WHEN '2' THEN 'Door Opened' WHEN '3' THEN 'Door Left Open' WHEN '4' THEN 'Door Closed' WHEN '5'" +
                            "THEN 'Door Forced Open' WHEN '6' THEN 'Mains Power Fail' WHEN '7' THEN 'Mains Power Restored' WHEN '8' THEN 'Tamper Open' WHEN '9' THEN 'Tamper Restored' WHEN '10'" +
                            "THEN 'Door Lock Power Failure' WHEN '11' THEN 'Door Lock Power Restored' WHEN '12' THEN 'Low Battery' WHEN '13' THEN 'Aux Input 1 Triggered' WHEN '14' THEN 'Aux Input 1 Restored'" +
                            "WHEN '15' THEN 'Aux Input 2 Triggered' WHEN '16' THEN 'Aux Input 2 Restored' WHEN '17' THEN 'Aux Input 3 Triggered' WHEN '18' THEN 'Aux Input 3 Restored' WHEN '19' THEN 'Aux Input 4 Triggered'" +
                            "WHEN '20' THEN 'Aux Input 4 Restored' WHEN '21' THEN 'Anti-passback Violation' WHEN '22' THEN 'Controller Memory almost Full (90%)' WHEN '23' THEN 'Controller Memory Full' WHEN '24' THEN '24'" +
                            "ELSE NULL END AS Event_Alarm_Type,CASE AE.Event_Alarm_Action  WHEN '01' THEN '01' WHEN '02' THEN '02' WHEN '03' THEN '03' WHEN '04' THEN '04' WHEN '05' THEN '05'" +
                            "ELSE NULL END AS Event_Alarm_Action,Event_Card_Code from [UNOMVC_TRANSACTION].[dbo].[ACS_Events] AE with(nolock) inner join ACS_CONTROLLER AC on CTLR_ID= AE.Event_Controller_ID";
            if (Event_Type!=0)
            {
                query += " WHERE AE.Event_Type=" + Event_Type;
            }
           
            if (Level!=0)
            {
                string setLevel = Level == 1 ? "1" : Level == 2?"0":"1";
                string query_ext = " and AC.CLTR_FOR_TA=" + Convert.ToInt32(setLevel);
                if (Event_Type == 0)
                {
                    query_ext = query_ext.Replace("and", "where");
                }
                query += query_ext ;
            }
            query += " order by Event_Datetime desc";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<EventBrowserDetails> entEventList = new List<EventBrowserDetails>();

            foreach (DataRow dr in x.Rows)
            {
                EventBrowserDetails entEvent = new EventBrowserDetails();
                entEvent.Event_ID = Convert.ToInt32(dr["Event_ID"]);
                entEvent.Event_Type = Convert.ToString(dr["Event_Type"]);
                entEvent.Event_Datetime = Convert.ToDateTime(dr["Event_Datetime"]);
                entEvent.Emp_Name = Convert.ToString(dr["Empname"]);
                entEvent.Event_Employee_Code = Convert.ToString(dr["Event_Employee_Code"]);
                entEvent.ReaderD = Convert.ToString(dr["ReaderD"]);
                entEvent.CtrlD = Convert.ToInt32(dr["CtrlD"]);
                entEvent.Event_Status = Convert.ToString(dr["Event_Status"]);
                entEvent.Event_Alarm_Type = Convert.ToString(dr["Event_Alarm_Type"]);
                entEvent.Event_Alarm_Action = Convert.ToString(dr["Event_Alarm_Action"]);
                entEvent.Event_Card_Code = Convert.ToString(dr["Event_Card_Code"]);
                entEventList.Add(entEvent);
            }
            return entEventList;

        }
    }
}