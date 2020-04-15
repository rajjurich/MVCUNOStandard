using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class LeaveFile
    {
        public int Leave_File_ID { get; set; }
        public string Leave_CODE { get; set; }
        public string Leave_Description { get; set; }
        public int Leave_IsPaid { get; set; }
        public string Leave_Group { get; set; }
        public int Leave_ISDELETED { get; set; }
        public DateTime Leave_DELETEDDATE { get; set; }
        public int Leave_IsProDataBasiss { get; set; }
        public int MAXCARRYFORWARD { get; set; }
        public int IS_SYNC { get; set; }
        public int COMPANY_ID { get; set; }
        public string Leave_IsPaid_desc { get; set; }
        public string Leave_Group_Desc { get; set; }

        public string ipaddress { get; set; }

    }
}