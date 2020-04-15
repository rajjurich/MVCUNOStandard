using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class ShiftPattern
    {
        public int SHIFT_PATTERN_ID { get; set; }
        public string SHIFT_PATTERN_CODE { get; set; }
        public string SHIFT_PATTERN_DESCRIPTION { get; set; }
        public string SHIFT_PATTERN_TYPE { get; set; }
        public string SHIFT_PATTERN { get; set; }
        public Nullable<bool> SHIFT_ISDELETED { get; set; }
        public Nullable<System.DateTime> SHIFT_DELETEDDATE { get; set; }
        public int COMPANY_ID { get; set; }
        public bool IS_SYNC { get; set; }

        public string ipaddress { get; set; }
    }
}