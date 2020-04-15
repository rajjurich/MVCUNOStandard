using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AutoCardLookUp
    {
        public DateTime Event_DateTime { get; set; }
        public string Event_Card_Code { get; set; }
        public string Emp_Name { get; set; }
        public string STATUS { get; set; }
        public string CTLR_READER_ID { get; set; }
        public string READER_DESCRIPTION { get; set; }

    }
}