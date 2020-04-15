using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AcsController
    {
        public string ipaddress { get; set; }
        [Key]
        public long ID { get; set; }
        [Required]
        public int CTLR_ID { get; set; }
        [Required]
        public string CTLR_DESCRIPTION { get; set; }
        [Required]
        public string CTLR_TYPE { get; set; }
        [Required]
        public string CTLR_IP { get; set; }
        public string CTLR_SUBNET_MASK { get; set; }
        public string CTLR_GATEWAY_IP { get; set; }
        public string CTLR_SERVER_IP { get; set; }
        public string CTLR_MAC_ID { get; set; }
        [Required]
        public string CTLR_INCOMING_PORT { get; set; }
        [Required]
        public string CTLR_OUTGOING_PORT { get; set; }
        public string CTLR_FIRMWARE_VERSION_NO { get; set; }
        public string CTLR_HARDWARE_VERSION_NO { get; set; }
        public Nullable<bool> CTLR_CHK_FACILITY_CODE { get; set; }
        public string CTLR_FACILITY_CODE1 { get; set; }
        public string CTLR_FACILITY_CODE2 { get; set; }
        public string CTLR_FACILITY_CODE3 { get; set; }
        public string CTLR_FACILITY_CODE4 { get; set; }
        public string CTLR_FACILITY_CODE5 { get; set; }
        public string CTLR_FACILITY_CODE6 { get; set; }
        [Required]
        public string CTLR_CHK_APB { get; set; }
        [Required]
        public string CTLR_APB_TYPE { get; set; }
        public string CTLR_APB_TIME { get; set; }
        [Required]
        public string CTLR_AUTHENTICATION_MODE { get; set; }
        public Nullable<bool> CTLR_CHK_TOC { get; set; }
        public string CTLR_EVENTS_STORED { get; set; }
        public string CTLR_MAX_TRANSACTIONS { get; set; }
        public string CTLR_CURRENT_USER_CNT { get; set; }
        public string CTLR_MAX_USER_CNT { get; set; }
        public Nullable<bool> CTLR_ISDELETED { get; set; }
        public Nullable<System.DateTime> CTLR_DELETEDDATE { get; set; }
        public string CTLR_DELETEDBY { get; set; }
        public Nullable<System.DateTime> CTLR_CREATEDDATE { get; set; }
        public string CTLR_CREATEDBY { get; set; }
        public Nullable<System.DateTime> CTLR_MODIFIEDDATE { get; set; }
        public string CTLR_MODIFIEDBY { get; set; }
        public string CTLR_CONN_STATUS { get; set; }
        public string CTLR_INACTIVE_DATETIME { get; set; }
        public bool CLTR_FOR_TA { get; set; }
        public Nullable<bool> Reinit_AL { get; set; }
        public Nullable<bool> Reinit_TZ { get; set; }
        public Nullable<bool> Reinit_AP { get; set; }
        public Nullable<bool> CTLR_KEY_PAD { get; set; }
        public string CTLR_LOCATION_ID { get; set; }
        public Nullable<bool> EmptyReaderStatus { get; set; }
        public Nullable<bool> CTLR_VISITOR { get; set; }
        [Required]
        public Nullable<int> COMPANY_ID { get; set; }
        public virtual ICollection<AccessPointDetails> AccessPointDetails { get; set; }        
        public virtual ICollection<AcsReader> Readers { get; set; }       
    }
}