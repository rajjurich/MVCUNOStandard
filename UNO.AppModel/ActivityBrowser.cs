using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class ActivityBrowser
    {
        public string ControllerID{ get; set; }
        public bool isRefresh { get; set; }
        public List<ActivityBrowserDetails> ActivityDetails { get; set; }
    }
    public class ActivityBrowserDetails
    {
        public DateTime DateTime { get; set; }
        public string CTRL_DESCRIPTION { get; set; }
        public string EVENT_DESC { get; set; }
        public string STATUS { get; set; }
        public string RETRY { get; set; }
        public string USER_ID { get; set; }
    }
}
