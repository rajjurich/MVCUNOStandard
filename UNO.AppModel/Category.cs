using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class Category
    {
        [Key]
        public int CATEGORY_ID { get; set; }
        [Required(ErrorMessage="Please provide Category")]
        [Display(Name="Category")]
        public int ORG_CATEGORY_ID { get; set; }
        [Display(Name = "Early Going allowed by")]        
        [DataType(DataType.Time)]
        [Required(ErrorMessage="required")]
        public TimeSpan? EARLY_GOING { get; set; }
        [Display(Name = "Late Coming allowed by")]        
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "required")]
        public TimeSpan? LATE_COMING { get; set; }
        [Display(Name = "Extra Hours (Click If Extra Allowed)")]
        public bool EXTRA_CHECK { get; set; }
        [RequiredIf("EXTRA_CHECK", "1", "Please Provide Extra Hrs Allowed(Before shift Hours)")]
        [Display(Name = "Extra Hrs Allowed(Before shift Hours)>=")]        
        [DataType(DataType.Time)]
        public TimeSpan? EXHRS_BEFORE_SHIFT_HRS { get; set; }
        [RequiredIf("EXTRA_CHECK", "1", "Please Provide Extra Hrs Allowed(After shift Hours)")]
        [Display(Name="Extra Hrs Allowed(After shift Hours)<=")]        
        [DataType(DataType.Time)]
        public TimeSpan? EXHRS_AFTER_SHIFT_HRS { get; set; }
        [Display(Name = "Compensatory Off")]
        public string COMPENSATORYOFF_CODE { get; set; }
        [Display(Name = "Early Going Hours")]
        public bool DED_FROM_EXHRS_EARLY_GOING { get; set; }
        [Display(Name = "Late Coming Hours")]
        public bool DED_FROM_EXHRS_LATE_COMING { get; set; }
        
        public string CREATEDBY { get; set; }
        
        public string MODIFIEDBY { get; set; }
        public bool ISDELETED { get; set; }
        
        public string DELETEDBY { get; set; }
        [Required(ErrorMessage = "Please provide Company")]
        [Display(Name = "Company")]
        public int COMPANY_ID { get; set; }

        public string ipaddress { get; set; }
        
    }
}
