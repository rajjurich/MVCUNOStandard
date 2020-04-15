using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class WeeklyOff
    {
        public int MWK_CD { get; set; }
        public string MWK_DESC { get; set; }
        public int MWK_DAY { get; set; }
        public int MWK_OFF { get; set; }
        public string MWK_PAT { get; set; }
        public bool FSTMWK { get; set; }
        public bool SECMWK { get; set; }
        public bool THDMWK { get; set; }
        public bool FURMWK { get; set; }
        public bool FIFMWK { get; set; }
        public string MWK_DAY_NAME { get; set; }
        public string MWK_OFF_NAME { get; set; }
        public int COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }

        public string ipaddress { get; set; }

    }
}