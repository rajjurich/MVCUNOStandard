using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class AcsControllerDto
    {        
        public long ID { get; set; }       
        public int CTLR_ID { get; set; }        
        public string CTLR_DESCRIPTION { get; set; }        
        public string CTLR_TYPE { get; set; }      
        public string CTLR_IP { get; set; }       
        //public string CTLR_MAC_ID { get; set; }       
        //public string CTLR_INCOMING_PORT { get; set; }       
        //public string CTLR_OUTGOING_PORT { get; set; }        
        public string CTLR_FIRMWARE_VERSION_NO { get; set; }        
        //public string CTLR_HARDWARE_VERSION_NO { get; set; }       
        //public string CTLR_CHK_APB { get; set; }        
        //public string CTLR_APB_TYPE { get; set; }        
        //public string CTLR_APB_TIME { get; set; }       
        //public string CTLR_AUTHENTICATION_MODE { get; set; }        
        //public bool CTLR_CHK_TOC { get; set; }
        public string CTLR_CONN_STATUS { get; set; }
        public string CTLR_INACTIVE_DATETIME { get; set; }
        public string CTLR_EVENTS_STORED { get; set; }        
        public string CTLR_CURRENT_USER_CNT { get; set; }
        //public bool CLTR_FOR_TA { get; set; }        
        public string CTLR_KEY_PAD { get; set; }        
        public int COMPANY_ID { get; set; }        
    }
}