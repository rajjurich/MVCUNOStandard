using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class LocationModel
    {
        [Key]
        public int COMPANY_ID { get; set; }
        [Display(Name = "Company Name")]
        [MaxLength(30)]
        [StringLength(30)]
        [Required(ErrorMessage = "Please Enter Company Name")]
        public string COMPANY_NAME { get; set; }
        [Display(Name = "Company Code")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Please Enter Company Code")]
        public string COMPANY_CODE { get; set; }
        public int COMPANY_ADDRESS_ID { get; set; }

        [Display(Name = "Company Address")]
        [MaxLength(150)]
        [StringLength(150)]
        [Required(ErrorMessage = "Please Enter Company Address")]
        public string COMPANY_ADDRESS { get; set; }

        [Display(Name = "Company City")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Please Enter Company City")]
        public string COMPANY_CITY { get; set; }

        [Display(Name = "Company Pin")]
        [MaxLength(10)]
        [StringLength(10)]
        [Required(ErrorMessage = "Please Enter Company Pin")]
        public string COMPANY_PIN { get; set; }

        [Display(Name = "Company Phone 1")]
        [MaxLength(15)]
        [StringLength(15)]
        [Required(ErrorMessage = "Please Enter Company Phone 1")]
        public string COMPANY_PHONE1 { get; set; }

        [Display(Name = "Company Phone 2")]
        [MaxLength(15)]
        [StringLength(15)]
        [Required(ErrorMessage = "Please Enter Company Phone 2")]
        public string COMPANY_PHONE2 { get; set; }

        [Display(Name = "Company State")]
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Please Select Company State")]
        public string COMPANY_STATE { get; set; }

        //[Display(Name = "Company Type")]
        ////[MaxLength(4)]
        //[Required(ErrorMessage = "Please Select Company Type")]
        public int ADDRESS_TYPE_ID { get; set; }

        //[Display(Name = "Company Type")]
        //[MaxLength(40)]
        //[Required(ErrorMessage = "Please Enter Company TYPE")]
        public string ADDRESS_TYPE { get; set; }
        [Display(Name = "State")]
        public string STATE_CODE { get; set; }
    }
}
