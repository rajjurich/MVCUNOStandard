using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class ActivityBrowser
    {
       
        public DateTime DateTime { get; set; }
        public string CTRL_DESCRIPTION { get; set; }
        public string EVENT_DESC { get; set; }
        public string STATUS { get; set; }
        public string RETRY { get; set; }
        public string USER_ID { get; set; }
    }
}