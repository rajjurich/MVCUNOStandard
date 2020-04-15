using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class CommonEntitiesModel
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Master")]
        [Required(ErrorMessage = "Please Select Common Master")]
        public int COMMON_TYPES_ID { get; set; }    
    
        public string COMMON_NAME { get; set; }

         [Display(Name = "Id")]
         [MaxLength(30)]
         [StringLength(30)]
         [Required(ErrorMessage = "Please Enter Id")]
         [RegularExpression("^[0-9A-Za-z]+$", ErrorMessage = "Invalid charachters")]
         public string OCE_ID { get; set; } //OCE_ID

         [Display(Name = "Description")]
         [MaxLength(30)]
         [StringLength(30)]
         [Required(ErrorMessage = "Please Enter Description")]
         [RegularExpression("[A-Za-z_.0-9- ]+", ErrorMessage = "Invalid charachters")]
         public string OCE_DESCRIPTION { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please Select Company")]
         public int COMPANY_ID { get; set; }

        public string COMPANY_NAME { get; set; }

        public string ipaddress { get; set; }

    }
}
