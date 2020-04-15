using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AccessPointDetails
    {        
        public decimal AP_ID { get; set; }
        public int CTLR_ID { get; set; }
        public int READER_ID { get; set; }
        public int DOOR_ID { get; set; }
        public string DOOR_TYPE { get; set; }
        public string DOOR_OPEN_DURATION { get; set; }
        public string DOOR_FEEDBACK_DURATION { get; set; }
    }
}