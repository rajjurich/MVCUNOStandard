using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Shift
    {
        public int SHIFT_ID { get; set; }
        public string SHIFT_CODE { get; set; }
        public string SHIFT_DESCRIPTION { get; set; }
        public string SHIFT_ALLOCATION_TYPE { get; set; }
        public Nullable<System.TimeSpan> SHIFT_AUTO_SEARCH_START { get; set; }
        public Nullable<System.TimeSpan> SHIFT_AUTO_SEARCH_END { get; set; }
        public string SHIFT_TYPE { get; set; }
        public Nullable<System.TimeSpan> SHIFT_START { get; set; }
        public Nullable<System.TimeSpan> SHIFT_END { get; set; }
        public Nullable<System.TimeSpan> SHIFT_BREAK_START { get; set; }
        public Nullable<System.TimeSpan> SHIFT_BREAK_END { get; set; }
        public Nullable<System.TimeSpan> SHIFT_BREAK_HRS { get; set; }
        public Nullable<System.TimeSpan> SHIFT_WORKHRS { get; set; }
        public Nullable<System.TimeSpan> SHIFT_HALFDAYWORKHRS { get; set; }
        public Nullable<bool> SHIFT_FLAG_ADD_BREAK { get; set; }
        public Nullable<bool> SHIFT_WEEKEND_DIFF_TIME { get; set; }
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_START { get; set; }
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_END { get; set; }
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_BREAK_START { get; set; }
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_BREAK_END { get; set; }
        public Nullable<System.TimeSpan> SHIFT_EARLY_SEARCH_HRS { get; set; }
        public Nullable<System.TimeSpan> SHIFT_LATE_SEARCH_HRS { get; set; }        
        public Nullable<System.DateTime> SHIFT_CREATEDDATE { get; set; }
        public string SHIFT_CREATEDBY { get; set; }
        public Nullable<System.DateTime> SHIFT_MODIFIEDDATE { get; set; }
        public string SHIFT_MODIFIEDBY { get; set; }
        public bool SHIFT_ISDELETED { get; set; }
        public Nullable<System.DateTime> SHIFT_DELETEDDATE { get; set; }
        public string SHIFT_DELETEDBY { get; set; }        
        public int COMPANY_ID { get; set; }

        public string ipaddress { get; set; }
    }
}