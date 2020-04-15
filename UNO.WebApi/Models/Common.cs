using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Common
    {
        public int ID { get; set; }
        public int COMMON_TYPES_ID { get; set; }
        public string COMMON_NAME { get; set; }
        public string OCE_ID { get; set; }
        public string OCE_DESCRIPTION { get; set; }
        public int Company_ID{ get; set; }
        public string Company_Name { get; set; }

        public string ipaddress { get; set; }

    }
}