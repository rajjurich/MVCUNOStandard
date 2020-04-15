using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EssTaLaReq
    {
        public int ESS_LA_ID { get; set; }

        public int ESS_LA_EMPID { get; set; }

        public DateTime ESS_LA_REQUESTDT { get; set; }

        public DateTime ESS_LA_FROMDT { get; set; }

        public DateTime ESS_LA_TODT { get; set; }

        public string ESS_LA_LVCD { get; set; }

        public int ESS_LA_REASON_ID { get; set; }

        public string ESS_LA_REMARK { get; set; }

        public string ESS_LA_SANCID { get; set; }

        public DateTime ESS_LA_SANCDT { get; set; }

        public string ESS_LA_SANC_REMARK { get; set; }

        public double ESS_LA_ORDER { get; set; }

        public string ESS_LA_STATUS { get; set; }

        public string ESS_LA_OLDSTATUS { get; set; }

        public int ESS_LA_ISDELETED { get; set; }

        public DateTime ESS_LA_DELETEDDATE { get; set; }

        public Double ESS_LA_LVDAYS { get; set; }

        public string ESS_REQUEST_TYPE { get; set; }

        public string NAME { get; set; }

        public string ipaddress { get; set; }
    }
}