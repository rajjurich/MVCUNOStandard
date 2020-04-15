using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Zone
    {
        [Key]
        public int ZONE_ID { get; set; }
        [Required]
        public string ZONE_DESCRIPTION { get; set; }
        public Nullable<bool> ZONE_ISDELETED { get; set; }
        public Nullable<System.DateTime> ZONE_DELETEDDATE { get; set; }
        public string ZONE_DELETEDBY { get; set; }
        public Nullable<System.DateTime> ZONE_CREATEDDATE { get; set; }
        public string ZONE_CREATEDBY { get; set; }
        public Nullable<System.DateTime> ZONE_MODIFIEDDATE { get; set; }
        public string ZONE_MODIFIEDBY { get; set; }
        public Nullable<int> COMPANY_ID { get; set; }
        public bool IS_SYNC { get; set; }
        public virtual ICollection<AcsReaderInfo> acsReaderInfos { get; set; } 
        public virtual ICollection<ZoneReaderRel> ZoneReaderRel { get; set; }

        public string ipaddress { get; set; }
    }   
}