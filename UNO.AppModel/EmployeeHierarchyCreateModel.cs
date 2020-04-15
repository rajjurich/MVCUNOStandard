using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeHierarchyCreateModel
    {        
        [Required(ErrorMessage="Please provide Reporting Person")]
        [Display(Name = "Reporting Person")]
        public int Hier_Mgr_ID { get; set; }        
        [Required(ErrorMessage="No Employee Selected")]
        public List<EmployeeInfo> Employees { get; set; }

        public string ipaddress { get; set; }
    }
}
