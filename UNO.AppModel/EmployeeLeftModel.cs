using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeLeftModel
    {
        [Key]
        public int EL_COLUMNID { get; set; }

        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Please Select Employee")]
        public int EL_EMP_ID { get; set; }


        [Display(Name = "Joining Date")]
        public DateTime EL_JOINING_DATE { get; set; }


        [Display(Name = "Left Date")]
        public DateTime EL_LEFT_DATE { get; set; }

        public int EL_ISDELETED { get; set; }
        public DateTime EL_DELETEDDATE { get; set; }

        [Display(Name = "Reason")]
        [Required(ErrorMessage = "Please Select Reason")]
        public int EL_REASONID { get; set; }

        public string EOD_EMPID { get; set; }
        public string REASON_DESC { get; set; }

        public string ipaddress { get; set; }

    }
}
