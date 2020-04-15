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
    public interface IActivityBrowserService
    {
        List<ActivityBrowser> GetActivity(string table, string whereclause);
    }


    public class ActivityBrowserService : IActivityBrowserService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public ActivityBrowserService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public List<ActivityBrowser> GetActivity(string table , string whereclause)
        {
            string query = "select DATETIME,C.CTLR_DESCRIPTION,substring(EVENT_DESC,0,len(EVENT_DESC)-3), "+ 
                            "case "+   
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0101' then 'Device Status' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0103' then 'Set Datetime' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0105' then 'Get Datetime' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='010B' then 'Device Configuration' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0111' then 'Access level' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0113' then 'Timezones' "+  
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0115' then 'Holiday List' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0117' then 'Access Point' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0119' then 'Input Point' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='011B' then 'Output Point' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='011D' then 'User Profile' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0121' then 'Anti-Passback' "+
                            "when substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC))='0123' then 'Reset All Anti-Passback' "+
                            "else substring(EVENT_DESC,len(EVENT_DESC)-3,len(EVENT_DESC)) "+
                            "end as EVENT_DESC ,STATUS,RETRY,USER_ID "+
                            "from " + table + " E LEFT OUTER JOIN ACS_CONTROLLER C ON E.CTLR_ID=C.CTLR_ID "+
                            "where Convert(varchar(20), E.[DATETIME], 101)= Convert(varchar(20), getdate(), 101) "+
                             whereclause +" order by E.DATETIME desc " ;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ActivityBrowser> entActivityList = new List<ActivityBrowser>();

            foreach (DataRow dr in x.Rows)
            {
                ActivityBrowser entActivity = new ActivityBrowser();
                entActivity.DateTime = Convert.ToDateTime(dr["DateTime"]);
                entActivity.CTRL_DESCRIPTION = Convert.ToString(dr["CTLR_DESCRIPTION"]);
                entActivity.EVENT_DESC = Convert.ToString(dr["EVENT_DESC"]);
                entActivity.STATUS = Convert.ToString(dr["STATUS"]);
                entActivity.RETRY = Convert.ToString(dr["RETRY"]);
                entActivity.USER_ID = Convert.ToString(dr["USER_ID"]);
                entActivityList.Add(entActivity);
            }
            return entActivityList;

        }
    }
}