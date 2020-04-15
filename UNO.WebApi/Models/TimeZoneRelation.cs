using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class TimeZoneRelation
    {
        public long TZ_RELATION_ID { get; set; }
        public Nullable<long> TZ_ID { get; set; }
        public string TZ_TYPE { get; set; }
        public string TZ_DAYOFWEEK { get; set; }
        public System.DateTime TZ_FROMTIME { get; set; }
        public System.DateTime TZ_TOTIME { get; set; }
        public bool TZR_ISDELETED { get; set; }
        public Nullable<System.DateTime> TZR_DELETEDDATE { get; set; }
        public Nullable<long> Period_id { get; set; }
        public Nullable<long> TZ_Flag { get; set; }
    }
}