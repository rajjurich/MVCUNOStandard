using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EmployeeCardConfig
    {

        public int CC_EMP_ID { get; set; }
        public string CARD_CODE { get; set; }

        public string FingureForTA { get; set; }
        public string PIN { get; set; }
        public int USE_COUNT { get; set; }
        public bool IGNORE_APB { get; set; }
        public bool STATUS { get; set; }
        public DateTime ACTIVATION_DATE { get; set; }
        public DateTime EXPIRY_DATE { get; set; }
        
        public string ID { get; set; }
        public string NAME { get; set; }

        public Boolean Remember { get; set; }

        public string ipaddress { get; set; }
    }
}