using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class VisitorAppointmentRequestModel
    {
        public int REQUEST_ID { get; set; }
        public string VISITOR_ID { get; set; }
       
        [Display(Name = "Approval Authority")]
        [Required(ErrorMessage = "Please select Approval")]
        [MaxLength(20)]
        [StringLength(20)]
        public string APPROVAL_AUTHORITY_CODE { get; set; }
        public string APPROVAL_STATUS { get; set; }
        public DateTime APRROVAL_DATETIME { get; set; }

        [Display(Name = "Approver Remark")]
        [MaxLength(200)]
        [StringLength(200)]
        public string APPROVER_REMARKS { get; set; }

        [Display(Name = "Visitor Allowed From Time")]
        public DateTime VISITOR_ALLOWED_FROM_TIME { get; set; }

        [Display(Name = "Visitor Allowed To Time")]
        public DateTime VISITOR_ALLOWED_TO_TIME { get; set; }

        [Display(Name = "Appointment From Date")]
        public DateTime APPOINTMENT_FROM_DATE { get; set; }

        [Display(Name = "Appointment To Date")]
        public DateTime APPOINTMENT_TO_DATE { get; set; }

        [Display(Name = "Nature Of Work")]
        [MaxLength(30)]
        [StringLength(30)]
        [Required(ErrorMessage = "Please select Nature Of Work")]
        public string NATURE_OF_WORK { get; set; }

        [Display(Name = "Additional Info")]
        [MaxLength(200)]
        [StringLength(200)]
        public string ADDITIONAL_INFO { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "Please Enter First Name")]
        public string VISITOR_NAME { get; set; }

        [Display(Name = "Company Name")]
        [MaxLength(200)]
        [StringLength(200)]
        [Required(ErrorMessage = "Please Enter Company Name")]
        public string VISITORCOMPANY { get; set; }

        [Display(Name = "Mobile Number")]
        [MaxLength(20)]
        [StringLength(20)]
        [Required(ErrorMessage = "Please Mobile Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string MOBILENO { get; set; }
        public DateTime CREATEDON { get; set; }
        public string CREATEDBY { get; set; }

        [Display(Name = "Designation")]
        [MaxLength(200)]
        [StringLength(200)]
        [Required(ErrorMessage = "Please Enter Designation")]
        public string DESIGNATION { get; set; }

        [Display(Name = "CheckOut Time")]
        public DateTime CHECKEDOUTTIME { get; set; }

        [Display(Name = "Purpose Of Visit")]
        [MaxLength(200)]
        public string PURPOSEOFVISIT { get; set; }

        [Display(Name = "Middle Name")]
        [MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "Please Enter Middle Name")]
        public string VISITOR_MIDDLENAME { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string VISITOR_LASTNAME { get; set; }

        [Display(Name = "Nationality")]
        [MaxLength(10)]
        [StringLength(10)]
        [Required(ErrorMessage = "Please Select Nationality")]
        public string VISITOR_NATIONALITY { get; set; }

        [Display(Name = "Location")]
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Please Select Location")]
        public string VISITOR_LOCATION { get; set; }
        public string TOTAL_COUNT { get; set; }
        public string IS_SYNC { get; set; }
        public int COMPANY_ID { get; set; }

        public string ipaddress { get; set; }

    }
}
