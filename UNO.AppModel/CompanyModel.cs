using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class CompanyModel
    {
        [Key]
        public int COMPANY_ID { get; set; }

        [Display(Name = "Company Name")]
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Please Enter Company Name")]
        [RegularExpression("[A-Za-z_.0-9- ]+", ErrorMessage = "Invalid charachters")]
        public string COMPANY_NAME { get; set; }

        [Display(Name = "Company Code")]
        [MaxLength(15)]
        [StringLength(15)]
        [Required(ErrorMessage = "Please Enter Company Code")]
        [RegularExpression("[A-Za-z_.0-9- ]+", ErrorMessage = "Invalid charachters")]
        public string COMPANY_CODE { get; set; }

        public List<CompanyLocationDetails> CompanyLocationList { get; set; }

        [Display(Name = "Company Code Length")]
        [Required(ErrorMessage = "Please Provide Length")]
        [Range(4, 10)]
        public int CompanyCodeLength { get; set; }
        public string ipaddress { get; set; }

    }
    public class CompanyLocationDetails
    {
        public string ipaddress { get; set; }
        public int COMPANY_ID { get; set; }
        public int COMPANY_ADDRESS_ID { get; set; }

        [Display(Name = "Company Address")]
        [MaxLength(150, ErrorMessage = "Maximum Length 150")]
        [StringLength(150, ErrorMessage = "Maximum Length 150")]
        [Required(ErrorMessage = "Please Enter Company Address")]
        [RegularExpression(@"^[#.0-9a-zA-Z\s,-]+$", ErrorMessage = "Invalid charachters")]
        public string COMPANY_ADDRESS { get; set; }

        [Display(Name = "Company City")]
        [MaxLength(20, ErrorMessage = "Maximum Length 20")]
        [StringLength(20, ErrorMessage = "Maximum Length 20")]
        [Required(ErrorMessage = "Please Enter Company City")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Invalid charachters")]
        public string COMPANY_CITY { get; set; }

        [Display(Name = "Company Pincode")]
        [MaxLength(6, ErrorMessage = "Maximum Length 6")]
        [StringLength(6, ErrorMessage = "Maximum Length 6")]
        [Required(ErrorMessage = "Please Enter Company Pincode")]
        [RegularExpression("[0-9]+", ErrorMessage = "Invalid charachters")]
        public string COMPANY_PIN { get; set; }

        [Display(Name = "Company Phone Number")]
        [MaxLength(15, ErrorMessage = "Maximum Length 15")]
        [StringLength(15, ErrorMessage = "Maximum Length 15")]
        [Required(ErrorMessage = "Please Enter Company Phone Number")]
        [RegularExpression("[0-9]+", ErrorMessage = "Invalid charachters")]
        public string COMPANY_PHONE1 { get; set; }

        [Display(Name = "Company Alternate Number")]
        [MaxLength(15, ErrorMessage = "Maximum Length 15")]
        [StringLength(15, ErrorMessage = "Maximum Length 15")]
        [Required(ErrorMessage = "Please Enter Company Alternate Number")]
        [RegularExpression("[0-9]+", ErrorMessage = "Invalid charachters")]
        public string COMPANY_PHONE2 { get; set; }

        [Display(Name = "Company State")]
        [MaxLength(50, ErrorMessage = "Maximum Length 50")]
        [StringLength(50, ErrorMessage = "Maximum Length 50")]
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
        [Required(ErrorMessage = "Please Select Company State")]
        public string STATE_CODE { get; set; }

        
    }
}
