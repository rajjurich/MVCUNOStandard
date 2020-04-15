using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AccessPointRelation
    {
        public decimal AP_ID { get; set; }
        public int READER_ID { get; set; }
        public int DOOR_ID { get; set; }
        public int AP_CONTROLLER_ID { get; set; }
        public bool APR_ISDELETED { get; set; }
        public Nullable<System.DateTime> APR_DELETEDDATE { get; set; }
        public Nullable<long> AccessPoint_Type { get; set; }
        public bool AP_FLAG { get; set; }
    }
}