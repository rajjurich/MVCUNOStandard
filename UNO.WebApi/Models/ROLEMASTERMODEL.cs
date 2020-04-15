using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class ROLEMASTERMODEL
    {
        public int rolemasterid { get; set; }
        public string ROLE_NAME { get; set; }

        public int COMPANY_ID { get; set; }

        public string Company_name { get; set; }
    }
}