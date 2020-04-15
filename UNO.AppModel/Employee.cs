using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class Employee
    {
        [Key]
        public int EMPLOYEE_ID { get; set; }
        [Display(Name = "Employee Code")]
        [Required(ErrorMessage = "Please Provide Employee Code")]
        [StringLength(10, ErrorMessage = "maximum length 10")]
        [RegularExpression("[A-Za-z0-9]+", ErrorMessage = "Invalid charachters")]
        public string EOD_EMPID { get; set; }
        [Display(Name = "Salutation")]
        [StringLength(5, ErrorMessage = "maximum length 5")]
        [RegularExpression("[A-Za-z.]+", ErrorMessage = "Invalid charachters")]
        public string EPD_SALUTATION { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please Provide First Name")]
        [StringLength(50, ErrorMessage = "maximum length 50")]
        [RegularExpression("[A-Za-z. ]+", ErrorMessage = "Invalid charachters")]
        public string EPD_FIRST_NAME { get; set; }
        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "maximum length 50")]
        [RegularExpression("[A-Za-z. ]+", ErrorMessage = "Invalid charachters")]
        public string EPD_MIDDLE_NAME { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Provide Last Name")]
        [StringLength(50, ErrorMessage = "maximum length 50")]
        [RegularExpression("[A-Za-z. ]+", ErrorMessage = "Invalid charachters")]
        public string EPD_LAST_NAME { get; set; }
        [Display(Name = "Perso Flag")]
        public string EPD_PERSO_FLAG { get; set; }
        [Display(Name = "Card Code")]
        [StringLength(10, ErrorMessage = "maximum length 10")]
        [RegularExpression("[A-Za-z0-9]+", ErrorMessage = "Invalid charachters")]
        public string EPD_CARD_ID { get; set; }
        [Display(Name = "Joining Date")]
        [Required(ErrorMessage = "Please Provide Joining Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EOD_JOINING_DATE { get; set; }
        [RequiredIf("EOD_TYPE", "C", "Please Provide Confirm Date")]
        [DateCompare("EOD_JOINING_DATE", "", " Confirm Date must be greater than or equal to Joining Date")]
        [Display(Name = "Confirm Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EOD_CONFIRM_DATE { get; set; }
        [RequiredIf("EOD_LEFT_REASON_ID", "S", "Please Provide Left Date")]
        [Display(Name = "Left Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EOD_LEFT_DATE { get; set; }
        [Display(Name = "Left Reason")]
        public Nullable<int> EOD_LEFT_REASON_ID { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Please Provide Company")]
        public Nullable<int> EOD_COMPANY_ID { get; set; }
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please Provide Location")]
        public Nullable<int> EOD_LOCATION_ID { get; set; }
        [Display(Name = "Division")]
        [Required(ErrorMessage = "Please Provide Division")]
        public Nullable<int> EOD_DIVISION_ID { get; set; }
        [Display(Name = "Department")]
        [Required(ErrorMessage = "Please Provide Department")]
        public Nullable<int> EOD_DEPARTMENT_ID { get; set; }
        [Display(Name = "Designation")]
        [Required(ErrorMessage = "Please Provide Designation")]
        public Nullable<int> EOD_DESIGNATION_ID { get; set; }
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please Provide Category")]
        public Nullable<int> EOD_CATEGORY_ID { get; set; }
        [Display(Name = "Group")]
        [Required(ErrorMessage = "Please Provide Group")]
        public Nullable<int> EOD_GROUP_ID { get; set; }
        [Display(Name = "Grade")]
        [Required(ErrorMessage = "Please Provide Grade")]
        public Nullable<int> EOD_GRADE_ID { get; set; }
        [Display(Name = "Employment Type")]
        [Required(ErrorMessage = "Please Provide Employment Type")]
        public string EOD_TYPE { get; set; }
        [Display(Name = "Previous Employee Code")]
        public Nullable<int> PREVIOUS_EMPLOYEE_ID { get; set; }
        [Required]
        [Display(Name = "Ess Enabled")]
        public bool EssEnabled { get; set; }
        [Display(Name = "Gender")]
        public Nullable<int> EPD_GENDER { get; set; }
        [Display(Name = "Marital Status")]
        public Nullable<int> EPD_MARITAL_STATUS { get; set; }
        [Display(Name = "Date of Birth")]        
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EPD_DOB { get; set; }
        [Display(Name = "Marriage Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EPD_DateOFMarriage { get; set; }
        [Display(Name = "Religion")]
        public Nullable<int> EPD_RELIGION { get; set; }
        [Display(Name = "Reference")]
        [StringLength(50, ErrorMessage = "Maximum length 50")]
        [RegularExpression("[A-Za-z0-9-. ,]+", ErrorMessage = "Invalid charachters")]
        public string EPD_REFERENCE_ONE { get; set; }
        [Display(Name = "Reference")]
        [StringLength(50, ErrorMessage = "Maximum length 50")]
        [RegularExpression("[A-Za-z0-9-. ,]+", ErrorMessage = "Invalid charachters")]
        public string EPD_REFERENCE_TWO { get; set; }
        [Display(Name = "Domicile")]
        [StringLength(50, ErrorMessage = "Maximum length 50")]
        public string EPD_DOMICILE { get; set; }
        [Display(Name = "Bloodgroup")]
        public Nullable<int> EPD_BLOODGROUP { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Maximum length 50")]
        public string EPD_EMAIL { get; set; }
        [Display(Name = "PAN")]
        [StringLength(10, ErrorMessage = "Maximum length 10")]
        [RegularExpression("[A-Z0-9]+", ErrorMessage = "Invalid charachters")]
        public string EPD_PAN { get; set; }
        public string EPD_PHOTOURL { get; set; }
        [Display(Name = "Aadhar Number")]
        [StringLength(12, ErrorMessage = "Maximum length 12")]
        [RegularExpression("[0-9]+", ErrorMessage = "Invalid charachters")]
        public string EPD_AADHARCARD { get; set; }
        [Display(Name = "UAN")]
        [StringLength(10, ErrorMessage = "Maximum length 10")]
        [RegularExpression("[A-Z0-9]+", ErrorMessage = "Invalid charachters")]
        public string EPD_UAN { get; set; }
        public string ACTIVE_USER { get; set; }
        public virtual List<EmployeeAddress> EmployeeAddresses { get; set; }
        [Display(Name = "Reporting Person")]
        public virtual List<EmployeeHierarchy> EmployeeHierarchy { get; set; }



        public string ipaddress { get; set; }
        //public virtual List<EmployeeNominee> EmployeeNominees { get; set; }
        //public virtual List<EmployeeFamily> EmployeeFamilies { get; set; }


    }
}
