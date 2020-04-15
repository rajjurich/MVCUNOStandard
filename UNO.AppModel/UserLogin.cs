using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class UserLogin
    {
        [Display(Name = "User Code")]
        [StringLength(15)]
        [Required(ErrorMessage = "Please Enter User ID")]
        public string USER_CODE { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(15)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        public int isReset { get; set; }

        public string verionsofwebapp { get; set; }
    }
}
