using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class HolidayDetails
    {
        public virtual int HOLIDAY_ID { get; set; }
        public virtual string HOLIDAY_CODE { get; set; }
        public virtual string HOLIDAY_DESCRIPTION { get; set; }
        public virtual string HOLIDAY_TYPE { get; set; }
        public virtual DateTime HOLIDAY_DATE { get; set; }
        public DateTime? HOLIDAY_SWAP { get; set; }
        public virtual int COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public string LocationWise { get; set; }
        public virtual string ACTIVE_USER { get; set; }
        public List<HolidayLocation> HolidayLoc { get; set; }
        public string ipaddress { get; set; }

    }

    public class HolidayLocation
    {
        public int HOLIDAYLOC_ID { get; set; }
        public int HOLIDAY_ID { get; set; }
        public int HOLIDAY_LOC_ID { get; set; }
        public int IS_OPTIONAL { get; set; }
        public int IS_SYNC { get; set; }
        public int COMPANY_ID { get; set; }
        public string OCE_ID { get; set; }
        public string OCE_DESCRIPTION { get; set; }
        public bool IS_bool_HOLIDAYLOC_ID { get; set; }

    }
}