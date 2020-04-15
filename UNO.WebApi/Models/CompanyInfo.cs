using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class CompanyInfo
    {
        public virtual int COMPANY_ID { get; set; }
        public virtual string COMPANY_CODE { get; set; }
        public virtual string COMPANY_NAME { get; set; }
       
    }
}