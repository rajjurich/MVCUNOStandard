using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
 public class EssTaMaReqModel
    {

        public int ESS_MA_ID { get; set; }
        public int ESS_MA_EMPID { get; set; }
        public DateTime ESS_MA_REQUESTDT { get; set; }

        [Display(Name = "From Date:")]
        [Required(ErrorMessage = "Please Select From Date")]
        public DateTime ESS_MA_FROMDT { get; set; }

        [Display(Name = "From Time:")]
        [Required(ErrorMessage = "Please Select From Time")]
        public DateTime ESS_MA_FROMTM { get; set; }
        
        [Display(Name = "To Date:")]
        [Required(ErrorMessage = "Please Select To Date")]
        public DateTime ESS_MA_TODT { get; set; }

        [Display(Name = "To Time:")]
        [Required(ErrorMessage = "Please Select To Time")]
        public DateTime ESS_MA_TOTM { get; set; }

        [Display(Name = "Reason:")]
        [Required(ErrorMessage = "Please Select Reason Code")]
        public int ESS_MA_REASON_ID { get; set; }

        [Display(Name = "Additional Info:")]
        public string ESS_MA_REMARK { get; set; }

        public int ESS_SANCTION_ID { get; set; }

        public DateTime ESS_MA_SANCDT { get; set; }

        public string ESS_MA_SANC_REMARK { get; set; }

        public double ESS_MA_ORDER { get; set; }

        public string ESS_MA_STATUS { get; set; }

        public string ESS_MA_OLDSTATUS { get; set; }

        public int ESS_MA_ISDELETED { get; set; }

        public DateTime ESS_MA_DELETEDDATE { get; set; }

        public double ESS_MA_LVDAYS { get; set; }
        
        public string ESS_REQUEST_TYPE { get; set; }
        
        public string NAME { get; set; }

        public string ipaddress { get; set; }
  
    }
}
