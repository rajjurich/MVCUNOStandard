using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Category
    {
        public int CATEGORY_ID { get; set; }
        public Nullable<int> ORG_CATEGORY_ID { get; set; }
        public Nullable<System.TimeSpan> EARLY_GOING { get; set; }
        public Nullable<System.TimeSpan> LATE_COMING { get; set; }
        public Nullable<bool> EXTRA_CHECK { get; set; }
        public Nullable<System.TimeSpan> EXHRS_BEFORE_SHIFT_HRS { get; set; }
        public Nullable<System.TimeSpan> EXHRS_AFTER_SHIFT_HRS { get; set; }
        public string COMPENSATORYOFF_CODE { get; set; }
        public Nullable<bool> DED_FROM_EXHRS_EARLY_GOING { get; set; }
        public Nullable<bool> DED_FROM_EXHRS_LATE_COMING { get; set; }
        public Nullable<System.DateTime> CREATEDDATE { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> MODIFIEDDATE { get; set; }
        public string MODIFIEDBY { get; set; }
        public Nullable<bool> ISDELETED { get; set; }
        public Nullable<System.DateTime> DELETEDDATE { get; set; }
        public string DELETEDBY { get; set; }
        public Nullable<int> COMPANY_ID { get; set; }
        public bool IS_SYNC { get; set; }

        public string ipaddress { get; set; }
    }
}