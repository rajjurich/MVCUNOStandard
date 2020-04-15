using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class CategoryInfo
    {
        public int CATEGORY_ID { get; set; }
        [Display(Name = "Category")]
        public string OCE_DESCRIPTION { get; set; }
        public int ORG_CATEGORY_ID { get; set; }
        [Display(Name = "Early Going")]
        public TimeSpan? EARLY_GOING { get; set; }
        [Display(Name = "Late Coming")]
        public TimeSpan? LATE_COMING { get; set; }
        [Display(Name = "ExHrs Before Shift")]
        public TimeSpan? EXHRS_BEFORE_SHIFT_HRS { get; set; }
        [Display(Name = "ExHrs After Shift")]
        public TimeSpan? EXHRS_AFTER_SHIFT_HRS { get; set; }
        [Display(Name = "Compensatory off Code")]
        public string COMPENSATORYOFF_CODE { get; set; }
        public int COMPANY_ID { get; set; }
    }
}
