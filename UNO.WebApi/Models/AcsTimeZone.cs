using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AcsTimeZone
    {
        public long TZ_ID { get; set; }
        public string TZ_DESCRIPTION { get; set; }
        public string TZ_NAME { get; set; }
        public bool TZ_ISDELETED { get; set; }
        public Nullable<System.DateTime> TZ_DELETEDDATE { get; set; }
        public Nullable<int> Period_id { get; set; }
    }
}