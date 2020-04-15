using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class AcsController
    {
        public string ipaddress { get; set; }
        [Key]
        public long ID { get; set; }
        [Display(Name = "Controller Id")]
        [Required(ErrorMessage = "Please Provide Controller Id")]
        [RegularExpression("^[0-9]{4}$", ErrorMessage = "Only Numbers allowed and It length of 4 digit only")]        
        public int CTLR_ID { get; set; }
        [Display(Name = "Controller Description")]
        [Required(ErrorMessage = "Please Provide Controller Description")]
        [StringLength(50, ErrorMessage = "Maximum Length 50")]
        public string CTLR_DESCRIPTION { get; set; }
        [Display(Name = "Controller Type")]
        [Required(ErrorMessage = "Please Provide Controller Type")]
        [StringLength(10, ErrorMessage = "Maximum Length 10")]
        public string CTLR_TYPE { get; set; }
        [Display(Name = "Controller IP")]
        [Required(ErrorMessage = "Please Provide Controller IP")]        
        [RegularExpression("\\b(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\\b", ErrorMessage = "Please provide valid IP Address")]
        [StringLength(15, ErrorMessage = "Please provide valid IP Address")]
        public string CTLR_IP { get; set; }
        [Display(Name = "MAC Id")]
        [StringLength(20, ErrorMessage = "Maximum Length 20")]
        public string CTLR_MAC_ID { get; set; }
        [Display(Name = "Incoming Port")]
        [Required(ErrorMessage = "Please Provide Incoming Port")]
        [RegularExpression("(6553[0-5]|655[0-2][0-9]\\d|65[0-4](\\d){2}|6[0-4](\\d){3}|[1-5](\\d){4}|[1-9](\\d){0,3})", ErrorMessage = "Please provide valid port number")]
        [StringLength(6, ErrorMessage = "Maximum Length 6")]
        public string CTLR_INCOMING_PORT { get; set; }
        [Display(Name = "Outgoing Port")]
        [Required(ErrorMessage = "Please Provide Outgoing Port")]
        [RegularExpression("(6553[0-5]|655[0-2][0-9]\\d|65[0-4](\\d){2}|6[0-4](\\d){3}|[1-5](\\d){4}|[1-9](\\d){0,3})", ErrorMessage = "Please provide valid port number")]
        [StringLength(6, ErrorMessage = "Maximum Length 6")]
        public string CTLR_OUTGOING_PORT { get; set; }
        [Display(Name = "Firmware Version No")]
        [StringLength(20, ErrorMessage = "Maximum Length 20")]
        public string CTLR_FIRMWARE_VERSION_NO { get; set; }
        [Display(Name = "Hardware Version No")]
        [StringLength(20, ErrorMessage = "Maximum Length 20")]
        public string CTLR_HARDWARE_VERSION_NO { get; set; }
        [Display(Name = "Local Antipassback")]
        [Required(ErrorMessage = "Please Provide Local Antipassback")]
        [StringLength(50, ErrorMessage = "Maximum Length 50")]
        public string CTLR_CHK_APB { get; set; }
        [Display(Name = "APB Schedule")]
        [Required(ErrorMessage = "Please Provide APB Schedule")]
        [StringLength(2, ErrorMessage = "Maximum Length 2")]
        public string CTLR_APB_TYPE { get; set; }
        [Display(Name = "Local Antipassback Time(Min)")]
        [StringLength(10, ErrorMessage = "Maximum Length 10")]
        public string CTLR_APB_TIME { get; set; }
        [Display(Name = "Authentication Mode")]
        [Required(ErrorMessage = "Please Provide Authentication Mode")]
        [StringLength(2, ErrorMessage = "Maximum Length 2")]
        public string CTLR_AUTHENTICATION_MODE { get; set; }
        [Display(Name = "Template On Card")]
        public bool CTLR_CHK_TOC { get; set; }
        [Display(Name = "Stored Events")]
        public string CTLR_EVENTS_STORED { get; set; }
        [Display(Name = "User Count")]
        public string CTLR_CURRENT_USER_CNT { get; set; }
        public string CTLR_CONN_STATUS { get; set; }
        public string CTLR_INACTIVE_DATETIME { get; set; }
        [Display(Name = "Time Attendance")]
        public bool CLTR_FOR_TA { get; set; }
        [Display(Name = "Key Pad")]
        public bool CTLR_KEY_PAD { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Please provide Company")]
        public int COMPANY_ID { get; set; }
        public bool CTLR_ISDELETED { get; set; }
        public string CTLR_CREATEDBY { get; set; }
        public string CTLR_MODIFIEDBY { get; set; }
        public string CTLR_DELETEDBY { get; set; }
        public List<AccessPointDetails> AccessPointDetails { get; set; }
        public List<AcsReader> Readers { get; set; }
    }
}
