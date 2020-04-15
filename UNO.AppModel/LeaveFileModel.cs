using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
   public class LeaveFileModel
    {
     [Key]
     public int Leave_File_ID{ get; set; }

     [Display(Name = "Leave Id")]
     [Required(ErrorMessage = "Please Enter Leave Id")]
     [StringLength(30, ErrorMessage = "Leave Id Length 30")]  
     public string Leave_CODE { get; set; }

     [Display(Name = "Description")]
     [Required(ErrorMessage = "Please Enter Description")]
     [StringLength(30, ErrorMessage = "Description Length 100")]  
     public string Leave_Description { get; set; }

     [Display(Name = "Paid Leave")]
     [Required(ErrorMessage = "Please Paid Leave")]
     public int Leave_IsPaid { get; set; }

     [Display(Name = "Leave Group")]
     [Required(ErrorMessage = "Please Select Leave Group")]
     public string Leave_Group { get; set; }
     public int Leave_ISDELETED { get; set; }
     public DateTime Leave_DELETEDDATE { get; set; }
     public int Leave_IsProDataBasiss { get; set; }
     public int MAXCARRYFORWARD { get; set; }
     public int IS_SYNC { get; set; }

     [Display(Name = "Company")]
     [Required(ErrorMessage = "select Company")]
     public int COMPANY_ID { get; set; }
     public string Leave_IsPaid_desc { get; set; }
     public string Leave_Group_Desc { get; set; }

     public string ipaddress { get; set; }
   }
}
