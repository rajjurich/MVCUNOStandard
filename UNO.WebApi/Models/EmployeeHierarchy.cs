using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EmployeeHierarchy
    {
        [Key]
        public int ENT_HierarchyDef_ID { get; set; }
        public Nullable<int> Hier_Emp_ID { get; set; }
        public string EmployeeName { get; set; }
        public Nullable<int> Hier_Mgr_ID { get; set; }
        public string ReportingName { get; set; }
        public Nullable<int> EOD_DESIGNATION_ID { get; set; }
        public string Designation { get; set; }
    }
}