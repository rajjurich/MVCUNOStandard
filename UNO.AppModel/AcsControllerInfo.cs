using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class AcsControllerInfo
    {
        public long ID { get; set; }
        [Display(Name = "Controller Id")]
        public int CTLR_ID { get; set; }
        [Display(Name = "Controller Description")]
        public string CTLR_DESCRIPTION { get; set; }
        [Display(Name = "Controller Type")]
        public string CTLR_TYPE { get; set; }
        [Display(Name = "Controller IP")]
        public string CTLR_IP { get; set; }        
        [Display(Name = "Stored Events")]
        public string CTLR_EVENTS_STORED { get; set; }
        [Display(Name = "User Count")]
        public string CTLR_CURRENT_USER_CNT { get; set; }
        [Display(Name = "Key Pad")]
        //public bool CLTR_FOR_TA { get; set; }        
        public string CTLR_KEY_PAD { get; set; }
        [Display(Name = "Company")]
        public int COMPANY_ID { get; set; }
        public string CTLR_FIRMWARE_VERSION_NO { get; set; }
        public string CTLR_CONN_STATUS { get; set; }
    }
}
