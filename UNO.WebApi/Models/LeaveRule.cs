using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class LeaveRule
    {
        public int LR_REC_ID { get; set; }
        public string LR_CODE { get; set; }
        public string LR_CATEGORYID { get; set; }
        public double LR_ALLOTMENT { get; set; }
        public double LR_ACCUMULATION { get; set; }
        public int LR_ISDELETED { get; set; }
        public DateTime LR_DELETEDDATE { get; set; }
        public string LeaveID { get; set; }
        public string LR_DAYS { get; set; }
        public string LEAVE_RULE { get; set; }
        public string LR_GreaterOrLesser { get; set; }
        public double LR_MinDaysAllowed { get; set; }
        public string LR_AllotmentType { get; set; }
        public string LR_AllotmentType_YE_PR { get; set; }
        public double LR_MaxDaysAllowed { get; set; }
        public int COMPANY_ID { get; set; }
        public string ipaddress { get; set; }

    }
}