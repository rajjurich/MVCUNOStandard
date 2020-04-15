using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EventBrowser
    {
        public int Event_Type { get; set; }
        public int Level_Id { get; set; }
        public bool isRefresh { get; set; }
        public List<EventBrowserDetails> EventDetails { get; set; }

    }
    public class EventBrowserDetails
    {
        public int Event_ID { get; set; }
        public string Event_Type { get; set; }
        public DateTime Event_Datetime { get; set; }
        public string Emp_Name { get; set; }
        public string Event_Employee_Code { get; set; }
        public string ReaderD { get; set; }
        public int CtrlD { get; set; }
        public string Event_Status { get; set; }
        public string Event_Alarm_Type { get; set; }
        public string Event_Alarm_Action { get; set; }
        public string Event_Card_Code { get; set; }
    }
}