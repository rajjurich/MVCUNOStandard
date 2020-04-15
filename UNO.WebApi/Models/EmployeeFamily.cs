using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EmployeeFamily
    {
        public int FAMILY_DETAIL_ID { get; set; }
        public Nullable<int> EMPLOYEE_ID { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Gender { get; set; }
        public string RELATIONTYPE { get; set; }
        public bool IS_SYNC { get; set; }
    }
}