using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeHierarchyInfo
    {
        public int ENT_HierarchyDef_ID { get; set; }
        [Display(Name="Employee Code")]
        public string EmployeeCode { get; set; }
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Reporting Name")]
        public string ReportingName { get; set; }        
        [Display(Name = "Reporting Code")]
        public string ReportingCode { get; set; }
        [Display(Name = "Reporting Designation")]
        public string Designation { get; set; }
    }
}
