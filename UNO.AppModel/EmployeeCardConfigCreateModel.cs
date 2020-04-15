using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UNO.AppModel
{
    public class EmployeeCardConfigCreateModel 
    {
        
        [Required(ErrorMessage="Required Use Count")]
        public int USECOUNT { get; set; }

        [Required(ErrorMessage="Required Activate Date")]
        public DateTime ACTIVATION_DATE { get; set; }
        [Required(ErrorMessage="Required Expiry Date")]
        [DateCompare("ACTIVATION_DATE", "", "Expiry date must be greater than or equal to Activation date")]
        public DateTime EXPIRY_DATE { get; set; }

        [Required(ErrorMessage="No Employee Selected")]
        public List<EmployeeInfo> Employees { get; set; }

        public Boolean Remember { get; set; }
        public string ipaddress { get; set; }



        //IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        //{
        //    List<ValidationResult> res = new List<ValidationResult>();
        //    if (EXPIRY_DATE < ACTIVATION_DATE)
        //    {
        //        ValidationResult mss = new ValidationResult("Expire date must be greater than or equal to Activation date");
        //        res.Add(mss);

        //    }
        //    return res;
        //}

       
    }
}
