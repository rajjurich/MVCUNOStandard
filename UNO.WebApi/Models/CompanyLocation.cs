using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class CompanyLocations
    {
        public int COMPANY_ID { get; set; }
        //public string COMPANY_NAME { get; set; }
        //public string COMPANY_CODE { get; set; }
        public int COMPANY_ADDRESS_ID { get; set; }
        public string COMPANY_ADDRESS { get; set; }
        public string COMPANY_CITY { get; set; }
        public string COMPANY_PIN { get; set; }
        public string COMPANY_PHONE1 { get; set; }
        public string COMPANY_PHONE2 { get; set; }
        public string COMPANY_STATE { get; set; }
        public string ADDRESS_TYPE { get; set; }
        public int ADDRESS_TYPE_ID { get; set; }
        public string STATE_CODE { get; set; }
    }
}