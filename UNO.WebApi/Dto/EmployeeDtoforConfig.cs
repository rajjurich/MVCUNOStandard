using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class EmployeeDtoforConfig
    {
        public int EMPLOYEE_ID { get; set; }
        public string EOD_EMPID { get; set; }
        public string FULL_NAME { get; set; }
        public string EOD_DESIGNATION { get; set; }
        public string EOD_JOINING_DATE { get; set; }
        public string EOD_STATUS { get; set; }
        public int? EOD_COMPANY_ID { get; set; }
        public int? EOD_LOCATION_ID { get; set; }
        public int? EOD_DIVISION_ID { get; set; }
        public int? EOD_DEPARTMENT_ID { get; set; }
    }
}