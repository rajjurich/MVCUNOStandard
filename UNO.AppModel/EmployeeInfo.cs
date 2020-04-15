using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeInfo
    {
        [Required(ErrorMessage="Employee Required")]
        public int EMPLOYEE_ID { get; set; }
        [Display(Name = "Employee Code")]
        public string EOD_EMPID { get; set; }
        [Display(Name = "Name")]
        public string FULL_NAME { get; set; }
        [Display(Name = "Designation")]
        public string EOD_DESIGNATION { get; set; }    
        [Display(Name = "Joining Date")]
        public string EOD_JOINING_DATE { get; set; }
        [Display(Name = "Status")]
        public string EOD_STATUS { get; set; }
        public int? EOD_COMPANY_ID { get; set; }
        public int? EOD_LOCATION_ID { get; set; }
        public int? EOD_DIVISION_ID { get; set; }
        public int? EOD_DEPARTMENT_ID { get; set; }
    }
}
