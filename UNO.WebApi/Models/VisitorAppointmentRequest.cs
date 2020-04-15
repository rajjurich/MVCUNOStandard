using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class VisitorAppointmentRequest
    {
        public int REQUEST_ID { get; set; }
        public string VISITOR_ID { get; set; }
        public string APPROVAL_AUTHORITY_CODE { get; set; }
        public string APPROVAL_STATUS { get; set; }
        public DateTime APRROVAL_DATETIME	{ get; set; }
        public string APPROVER_REMARKS { get; set; }
        public DateTime VISITOR_ALLOWED_FROM_TIME { get; set; }
        public DateTime VISITOR_ALLOWED_TO_TIME { get; set; }
        public DateTime APPOINTMENT_FROM_DATE { get; set; }
        public DateTime APPOINTMENT_TO_DATE { get; set; }
        public string NATURE_OF_WORK { get; set; }
        public string ADDITIONAL_INFO { get; set; }
        public string VISITOR_NAME { get; set; }
        public string VISITORCOMPANY { get; set; }
        public string MOBILENO { get; set; }
        public DateTime CREATEDON { get; set; }
        public string CREATEDBY { get; set; }
        public string DESIGNATION { get; set; }
        public DateTime CHECKEDOUTTIME { get; set; }
        public string PURPOSEOFVISIT { get; set; }
        public string VISITOR_MIDDLENAME { get; set; }
        public string VISITOR_LASTNAME { get; set; }
        public string VISITOR_NATIONALITY { get; set; }
        public string VISITOR_LOCATION { get; set; }
        public string TOTAL_COUNT { get; set; }
        public string IS_SYNC { get; set; }
        public int COMPANY_ID { get; set; }

        public string ipaddress { get; set; }


    }
}