using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class ReaderTemplate
    {
        public int RowID { get; set; }
        public int ControllerID { get; set; }
        public int EventID { get; set; }
        public string EventMessage { get; set; }
        public string ControllerName { get; set; }
        public string EventName { get; set; }
        public int PARAM_ID { get; set; }
        public string VALUE { get; set; }

        public string ipaddress { get; set; }

    }
}