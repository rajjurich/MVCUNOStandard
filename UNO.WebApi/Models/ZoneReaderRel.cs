using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class ZoneReaderRel
    {
        public Nullable<int> ZONE_ID { get; set; }
        public int READER_ID { get; set; }
        public int CONTROLLER_ID { get; set; }
        public Nullable<bool> ZONER_ISDELETED { get; set; }
        public Nullable<System.DateTime> ZONER_DELETEDDATE { get; set; }

        public virtual Zone ZONE { get; set; }
    }
}