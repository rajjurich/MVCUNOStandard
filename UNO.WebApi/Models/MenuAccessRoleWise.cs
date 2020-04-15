using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class MenuAccessRoleWise
    {
        //public long ROLE_ACCESS_ID { get; set; }
        public string ROLE_ID { get; set; }
        public int MODULE_ID { get; set; }
        public string MODULE_NAME { get; set; }
        public int MENU_ID { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_URL { get; set; }
        public double MENU_ITEMPOSITION { get; set; }
        public int SMODULE_ID { get; set; }
        public string SMODULE_NAME { get; set; }
    }
}