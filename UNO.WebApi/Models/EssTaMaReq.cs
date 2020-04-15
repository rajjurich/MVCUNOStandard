using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EssTaMaReq
    {
        public int ESS_MA_ID { get; set; }

        public int ESS_MA_EMPID { get; set; }

        public DateTime ESS_MA_REQUESTDT { get; set; }

        public DateTime ESS_MA_FROMDT { get; set; }

        public DateTime ESS_MA_TODT { get; set; }

        public int ESS_MA_REASON_ID { get; set; }

        public string ESS_MA_REMARK { get; set; }

        public string ESS_MA_SANCID { get; set; }

        public DateTime ESS_MA_SANCDT { get; set; }

        public string ESS_MA_SANC_REMARK { get; set; }

        public double ESS_MA_ORDER { get; set; }

        public string ESS_MA_STATUS { get; set; }

        public string ESS_MA_OLDSTATUS { get; set; }

        public int ESS_MA_ISDELETED { get; set; }

        public DateTime ESS_MA_DELETEDDATE { get; set; }

        public Double ESS_MA_LVDAYS { get; set; }

        public string ESS_REQUEST_TYPE { get; set; }

        public string NAME { get; set; }

        public string ipaddress { get; set; }
    }
}