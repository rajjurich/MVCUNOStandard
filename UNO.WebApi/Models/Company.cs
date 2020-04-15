using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Company
    {
        public virtual int COMPANY_ID { get; set; }
        public virtual string COMPANY_CODE { get; set; }
        public virtual string COMPANY_NAME { get; set; }
        public List<CompanyLocationDetails> CompanyLocationList { get; set; }

        public int CompanyCodeLength { get; set; }

        public string ipaddress { get; set; }
    }
    public class CompanyLocationDetails
    {
        public int COMPANY_ID { get; set; }
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

        public int CompanyCodeLength { get; set; }
    }
}