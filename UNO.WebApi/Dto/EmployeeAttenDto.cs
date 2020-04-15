using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class EmployeeAttenDto
    {
        public List<EmployeeDtoforConfig> Employees { get; set; }

        public int ETC_EMP_ID { get; set; }

        public string ETC_MINIMUM_SWIPE { get; set; }

        public string ETC_SHIFTCODE { get; set; }

        public string ETC_WEEKEND { get; set; }

        public string ETC_WEEKOFF { get; set; }

        public DateTime ETC_SHIFT_START_DATE { get; set; }

        public Boolean ETC_ISDELETED { get; set; }

        public DateTime ETC_DELETEDDATE { get; set; }

        public string ScheduleType { get; set; }

        public string ShiftType { get; set; }

        public Boolean IS_SYNC { get; set; }

        public string ipaddress { get; set; }
    }
}