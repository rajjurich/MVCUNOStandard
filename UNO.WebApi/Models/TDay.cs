using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class TDay
    {   
        public int TDAY_ID { get; set; }
        public Nullable<int> TDAY_EMPCDE { get; set; }
        public System.DateTime TDAY_DATE { get; set; }
        public string TDAY_SFTASSG { get; set; }
        public string TDAY_SFTREPO { get; set; }
        public Nullable<System.DateTime> TDAY_INTIME { get; set; }
        public Nullable<System.DateTime> TDAY_OUTIME { get; set; }
        public Nullable<System.DateTime> TDAY_LUNCH_OUT { get; set; }
        public Nullable<System.DateTime> TDAY_LUNCH_IN { get; set; }
        public Nullable<System.DateTime> TDAY_OUDATE { get; set; }
        public Nullable<System.DateTime> TDAY_EXHR { get; set; }
        public Nullable<int> TDAY_STATUS_ID { get; set; }
        public Nullable<int> TDAY_STATUS_ID_FH { get; set; }
        public Nullable<int> TDAY_STATUS_ID_SH { get; set; }
        public Nullable<System.DateTime> TDAY_LATE { get; set; }
        public Nullable<System.DateTime> TDAY_EARLY { get; set; }
        public Nullable<System.DateTime> TDAY_WRKHR { get; set; }
        public Nullable<decimal> TDAY_ERLNCH { get; set; }
        public Nullable<decimal> TDAY_LTLNCH { get; set; }
        public string TDAY_TOTIN { get; set; }
        public string TDAY_TOTOUT { get; set; }
        public string TDAY_ENTRY { get; set; }
        public string TDAY_LVCUT { get; set; }
        public Nullable<System.DateTime> TDAY_LTOT { get; set; }
        public Nullable<System.DateTime> TDAY_EROT { get; set; }
        public Nullable<int> TDAY_SHIFT_INDEX { get; set; }
        public Nullable<System.DateTime> TDAY_InDATE { get; set; }
        public string TDAY_SHIFT_PATTERN_ID { get; set; }
        public Nullable<int> isProcessed { get; set; }
        public Nullable<int> TDAY_LEAVE_YEAR { get; set; }

    }
}