using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Menu
    {
        public long MENU_ID { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_URL { get; set; }        
        public virtual int MODULE_ID { get; set; }
        public virtual string MODULE_NAME { get; set; }
        public virtual float MENU_ITEMPOSITION { get; set; }
        public virtual int SMODULE_ID { get; set; }
        public virtual string SMODULE_NAME { get; set; }

        public string ipaddress { get; set; }
    }
}