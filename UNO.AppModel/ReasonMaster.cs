using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class ReasonMaster
    {

        [Key]
        public int REASON_ID { get; set; }

        [Display(Name = "Reason Code")]
        [StringLength(10)]
        [Required(ErrorMessage = "Please Enter Reason Code")]
        [RegularExpression("[A-Za-z_.0-9- ]+", ErrorMessage = "Invalid charachters")]
        public string REASON_CODE { get; set; }

        [Display(Name = "Description")]
        [StringLength(100)]
        [Required(ErrorMessage = "Please Enter Description")]
        [RegularExpression("[A-Za-z_.0-9- ]+", ErrorMessage = "Invalid charachters")]
        public string REASON_DESC { get; set; }

        [Display(Name = "Reason Type")]
        [Required(ErrorMessage = "Please Select Reason Type")]
        public int REASON_TYPE_ID { get; set; }

        [Key]
        public string REASON_TYPE { get; set; }

        [Display(Name = "Company ")]
        [Required(ErrorMessage = "Please Select Company")]
        public int? COMPANY_ID { get; set; }
        [Key]
        public string COMPANY_NAME { get; set; }

        public string ipaddress { get; set; }
        //[Key]
        //public string ent_REASON_DESC { get; set; }
    }
}
