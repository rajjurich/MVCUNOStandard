using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeAccess
    {
        [Required(ErrorMessage="Please select Entity")]
        [Display(Name = "Entity")]        
        public int COMMON_TYPES_ID { get; set; }
        [Required(ErrorMessage="Please Provide Entities")]
        [Display(Name="Entities")]
        public virtual List<int> EntityIds { get; set; }
        [Required(ErrorMessage = "Please Provide Access Levels")]        
        [Display(Name="Access Level")]
        public List<long> AL_IDs { get; set; }

        public string ipaddress { get; set; }
    }
}
