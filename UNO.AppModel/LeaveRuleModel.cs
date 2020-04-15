using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
  public class LeaveRuleModel
    {
      public string ipaddress { get; set; }
        [Key]
          public int LR_REC_ID{ get; set; }
          public string LR_CODE { get; set; }

         [Display(Name = "Category ID")]
         [Required(ErrorMessage = "Please Select Category ID")]
         public string LR_CATEGORYID { get; set; }

          [Display(Name = "Allotted Days")]
         [Required(ErrorMessage = "Please Enter Allotted Days")]
         //[StringLength(9, ErrorMessage = "Allotted Days Length 9")]  
          public double LR_ALLOTMENT { get; set; }
 

         [Display(Name = "Accumulation Limit")]
         [Required(ErrorMessage = "Please Enter Accumulation Limit")]
         //[StringLength(9, ErrorMessage = "Accumulation Limit Length 9")]  
         public double LR_ACCUMULATION { get; set; }
         public int LR_ISDELETED { get; set; }
         public DateTime LR_DELETEDDATE { get; set; }

         [Display(Name = "Leave Code ")]
         [Required(ErrorMessage = "Please Select Leave Code")]
         public string LeaveID { get; set; }
         public string LR_DAYS { get; set; }
         public string LEAVE_RULE { get; set; }
         public string LR_GreaterOrLesser { get; set; }
      
         [Display(Name = "Minimum Days Allowed ")]
         [Required(ErrorMessage = "Please Enter Minimum Days Allowed ")]
         //[StringLength(9, ErrorMessage = "Minimum Days Allowed Length 9")]  
         public double LR_MinDaysAllowed { get; set; }

         [Display(Name = "Allotment Type")]
         [Required(ErrorMessage = "Please Select Allotment Type")]
         public string LR_AllotmentType { get; set; }
         public string LR_AllotmentType_YE_PR { get; set; }

         [Display(Name = "Maxmium Days Allowed ")]
         [Required(ErrorMessage = "Please Enter Maxmium Days Allowed ")]
         //[StringLength(9, ErrorMessage = "Maxmium Days Allowed Length 9")]
         public double LR_MaxDaysAllowed { get; set; }

         [Display(Name = "Company")]
         [Required(ErrorMessage = "select Company")]
         public int COMPANY_ID { get; set; }

    }
}
