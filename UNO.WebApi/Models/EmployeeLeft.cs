using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EmployeeLeft
    {
        public int EL_COLUMNID { get; set; }
        public int EL_EMP_ID { get; set; }
        public DateTime EL_LEFT_DATE { get; set; }
        public int EL_ISDELETED { get; set; }
        public DateTime EL_DELETEDDATE { get; set; }
        public int EL_REASONID { get; set; }
        public string EOD_EMPID { get; set; }
        public string REASON_DESC { get; set; }

        public string ipaddress { get; set; }

    }
}