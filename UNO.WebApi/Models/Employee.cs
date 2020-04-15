using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Employee
    {
        [Key]
        public int EMPLOYEE_ID { get; set; }
        [Required]
        public string EOD_EMPID { get; set; }
        public string EPD_SALUTATION { get; set; }
        [Required]
        public string EPD_FIRST_NAME { get; set; }
        public string EPD_MIDDLE_NAME { get; set; }
        [Required]
        public string EPD_LAST_NAME { get; set; }

        public int CompanyCodeLength { get; set; }
        public string EPD_PERSO_FLAG { get; set; }
        public string EPD_CARD_ID { get; set; }
        [Required]
        public Nullable<System.DateTime> EOD_JOINING_DATE { get; set; }
        public Nullable<System.DateTime> EOD_CONFIRM_DATE { get; set; }
        public Nullable<System.DateTime> EOD_LEFT_DATE { get; set; }
        public Nullable<int> EOD_LEFT_REASON_ID { get; set; }
        [Required]
        public Nullable<int> EOD_COMPANY_ID { get; set; }
        [Required]
        public Nullable<int> EOD_LOCATION_ID { get; set; }
        [Required]
        public Nullable<int> EOD_DIVISION_ID { get; set; }
        [Required]
        public Nullable<int> EOD_DEPARTMENT_ID { get; set; }
        [Required]
        public Nullable<int> EOD_DESIGNATION_ID { get; set; }
        [Required]
        public Nullable<int> EOD_CATEGORY_ID { get; set; }
        [Required]
        public Nullable<int> EOD_GROUP_ID { get; set; }
        [Required]
        public Nullable<int> EOD_GRADE_ID { get; set; }        
        public Nullable<int> EOD_STATUS { get; set; }
        public Nullable<bool> EOD_ISDELETED { get; set; }
        public Nullable<System.DateTime> EOD_DELETEDDATE { get; set; }
        [Required]
        public string EOD_TYPE { get; set; }
        public string EOD_WORKTYPE { get; set; }
        public string EOD_CARD_PIN { get; set; }
        public bool IS_SYNC { get; set; }
        public Nullable<int> PREVIOUS_EMPLOYEE_ID { get; set; }        
        [Required]
        public bool EssEnabled { get; set; }
        public Nullable<int> EPD_GENDER { get; set; }
        public Nullable<int> EPD_MARITAL_STATUS { get; set; }
        public Nullable<System.DateTime> EPD_DOB { get; set; }
        public Nullable<System.DateTime> EPD_DateOFMarriage { get; set; }
        public Nullable<int> EPD_RELIGION { get; set; }
        public string EPD_REFERENCE_ONE { get; set; }
        public string EPD_REFERENCE_TWO { get; set; }
        public string EPD_DOMICILE { get; set; }
        public Nullable<int> EPD_BLOODGROUP { get; set; }
        public string EPD_EMAIL { get; set; }
        public string EPD_PAN { get; set; }
        public string EPD_PHOTOURL { get; set; }
        public string EPD_AADHARCARD { get; set; }
        public string EPD_UAN { get; set; }
        public string ACTIVE_USER { get; set; }
        public virtual ICollection<EmployeeAddress> EmployeeAddresses { get; set; }
        public virtual ICollection<EmployeeNominee> EmployeeNominees { get; set; }
        public virtual ICollection<EmployeeFamily> EmployeeFamilies { get; set; }
        [Required]
        public virtual ICollection<EmployeeHierarchy> EmployeeHierarchy { get; set; }



        public string ipaddress { get; set; }
    }
}