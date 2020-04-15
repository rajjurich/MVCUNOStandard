using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeTimeAttendanceCreateModel
    {

        public int ETC_EMP_ID { get; set; }
        [Required(ErrorMessage = "Please provide Minimum  Swipe")]
        [Range(1, 99, ErrorMessage = "Minimum Swipe should be beetween 1 and 99")]
        public string ETC_MINIMUM_SWIPE { get; set; }
        [Required(ErrorMessage = "Please Select Shift Code")]
        public string ETC_SHIFTCODE { get; set; }
        [Required(ErrorMessage = "Please Select WeekEnd")]
        public string ETC_WEEKEND { get; set; }
        [Required(ErrorMessage = "Please Select WeekOff")]
        public string ETC_WEEKOFF { get; set; }
        [Required(ErrorMessage = "Please provide Shift Date")]
        public DateTime ETC_SHIFT_START_DATE { get; set; }

        public Boolean ETC_ISDELETED { get; set; }

        public DateTime ETC_DELETEDDATE { get; set; }

        public string ScheduleType { get; set; }

        public string ShiftType { get; set; }

        public Boolean IS_SYNC { get; set; }

        public List<EmployeeInfo> Employees { get; set; }

        public string ipaddress { get; set; }


    }
}
