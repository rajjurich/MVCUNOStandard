using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeHierarchy
    {
        [Key]
        public int ENT_HierarchyDef_ID { get; set; }
        [Display(Name = "Reporting Person")]
        public Nullable<int> Hier_Mgr_ID { get; set; }        
        [Display(Name = "Reporting Person Designation")]
        public Nullable<int> EOD_DESIGNATION_ID { get; set; }
        [Display(Name = "Employee")]
        public Nullable<int> Hier_Emp_ID { get; set; }
    }
}
