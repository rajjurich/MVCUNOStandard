using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class ReasonMaster
    {
        public int? REASON_TYPE_ID { get; set; }
        public int? REASON_ID { get; set; }
        public string REASON_CODE { get; set; }
        public string REASON_TYPE { get; set; }
        public string REASON_DESC { get; set; }
        public int? COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }

        public string ipaddress { get; set; }
    }
}