using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class ShiftPattern
    {
        public int SHIFT_PATTERN_ID { get; set; }
        [Display(Name="Shift Pattern Code")]
        [Required]
        public string SHIFT_PATTERN_CODE { get; set; }
        [Display(Name = "Shift Pattern Description")]
        [Required]
        public string SHIFT_PATTERN_DESCRIPTION { get; set; }
        [Display(Name = "Shift Pattern Type")]
        [Required]
        public string SHIFT_PATTERN_TYPE { get; set; }
        [Display(Name = "Shift Pattern")]
        [Required]
        public string SHIFT_PATTERN { get; set; }
        [Required(ErrorMessage = "Please provide Company")]
        [Display(Name = "Company")]
        public int COMPANY_ID { get; set; }

        public string ipaddress { get; set; }
    }
}
