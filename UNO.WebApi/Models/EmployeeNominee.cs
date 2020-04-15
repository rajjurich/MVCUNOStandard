using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EmployeeNominee
    {
        public int NOMINEE_DETAIL_ID { get; set; }
        public Nullable<int> EMPLOYEE_ID { get; set; }
        public string Nominee { get; set; }
        public string NomineesAddress { get; set; }
        public string Relation { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<decimal> SharePercent { get; set; }
        public string GuardianAddress { get; set; }
        public bool IS_SYNC { get; set; }
        public Nullable<int> NOMINEE_TYPE_ID { get; set; }
    }
}