using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class UserModel
    {
        public string ipaddress { get; set; }
        public long USER_ID { get; set; }

        [Display(Name = "User Code")]
        [MaxLength(10)]
        [StringLength(10)]
        [Required(ErrorMessage = "Please Enter User Code")]
        


        public string USER_CODE { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [RegularExpression("[A-Za-z0-9]{8,15}",ErrorMessage="Password must be alphanumeric with min 8 and max 15 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Confirm your Password")]
        [MaxLength(15)]
        [StringLength(15)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string Confirm_Password { get; set; }
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Please Select Role")]
        public int ROLE_ID { get; set; }
        [Display(Name = "Role")]
        public string ROLE_Name { get; set; }

        public bool EssEnabled { get; set; }
        [Display(Name = "Password Reset")]
        public bool PASSWORD_RESET { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Please Select Company")]
        public int COMPANY_ID { get; set; }
        [Display(Name = "Company Name")]
        public string COMPANY_NAME { get; set; }
        public string ACTIVE_USER { get; set; }

        public string Old_Password { get; set; }

        public int isReset { get; set; }
    }

}
