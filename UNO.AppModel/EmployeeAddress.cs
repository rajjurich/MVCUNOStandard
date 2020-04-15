using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeAddress
    {
        [Key]
        [Display(Name="Address Id")]
        public int EMPLOYEE_ADDRESS { get; set; }        
        [Display(Name = "Employee Code")]
        public Nullable<int> EMPLOYEE_ID { get; set; }
        [Display(Name = "Address Type")]
        public string EAD_ADDRESS_TYPE { get; set; }
        [Display(Name = "Address")]
        [StringLength(200,ErrorMessage="maximum length 200")]
        [RegularExpression("[A-Za-z0-9,.-_ ]+", ErrorMessage = "Invalid charachters")]
        public string EAD_ADDRESS { get; set; }
        [Display(Name = "City")]
        [StringLength(50, ErrorMessage = "maximum length 50")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Invalid charachters")]
        public string EAD_CITY { get; set; }
        [Display(Name = "Pincode")]
        [StringLength(6, ErrorMessage = "maximum length 6")]
        [RegularExpression("[0-9]+", ErrorMessage = "Invalid charachters")]
        public string EAD_PIN { get; set; }
        [Display(Name = "State")]
        public string EAD_STATE { get; set; }
        [Display(Name = "Country")]
        [StringLength(50, ErrorMessage = "maximum length 50")]
        [RegularExpression("[A-Za-z0-9 ,.]+", ErrorMessage = "Invalid charachters")]
        public string EAD_COUNTRY { get; set; }
        [Display(Name = "Mobile Number")]
        [StringLength(15, ErrorMessage = "maximum length 15")]
        [RegularExpression("[0-9]+", ErrorMessage = "Invalid charachters")]
        public string EAD_PHONE_ONE { get; set; }
        [StringLength(15, ErrorMessage = "maximum length 15")]
        [RegularExpression("[0-9]+", ErrorMessage = "Invalid charachters")]
        [Display(Name = "Alternate Number")]
        public string EAD_PHONE_TWO { get; set; }        
    }
}
