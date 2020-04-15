using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class Shift
    {
        [Key]
        public int SHIFT_ID { get; set; }
        [Required(ErrorMessage = "Please provide Shift")]
        [Display(Name = "Shift ID")]
        public string SHIFT_CODE { get; set; }
        [Required(ErrorMessage = "Please provide Shift Description")]
        [Display(Name = "Shift Description")]
        public string SHIFT_DESCRIPTION { get; set; }        
        [Display(Name = "Shift Allocation Type")]
        public string SHIFT_ALLOCATION_TYPE { get; set; }
        [Display(Name = "Shift Auto Search Start Time")]
        [DataType(DataType.Time)]         
        public Nullable<System.TimeSpan> SHIFT_AUTO_SEARCH_START { get; set; }
        [Display(Name = "Shift Auto Search End Time")]
        [DataType(DataType.Time)]        
        public Nullable<System.TimeSpan> SHIFT_AUTO_SEARCH_END { get; set; }
        [Required(ErrorMessage="Please Select Shift Type")]
        [Display(Name = "Shift Type")]        
        public string SHIFT_TYPE { get; set; }
        [Required(ErrorMessage = "Please Provide Shift Start")]
        [Display(Name = "Shift Start")]
        [DataType(DataType.Time)]        
        public Nullable<System.TimeSpan> SHIFT_START { get; set; }
        [Required(ErrorMessage = "Please Provide Shift End")]
        [Display(Name = "Shift End")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_END { get; set; }
        [Required(ErrorMessage = "Please Provide Break Start")]
        [Display(Name = "Break Start")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_BREAK_START { get; set; }
        [Required(ErrorMessage = "Please Provide Break End")]
        [Display(Name = "Break End")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_BREAK_END { get; set; }
        [Required(ErrorMessage = "Please Provide Total Break Hours")]
        [Display(Name = "Total Break Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_BREAK_HRS { get; set; }
        [Required(ErrorMessage = "Please Provide Total Work Hours")]
        [Display(Name = "Total Work Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_WORKHRS { get; set; }
        [Required(ErrorMessage = "Please Provide Halfday Work Hours")]
        [Display(Name = "Halfday Work Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_HALFDAYWORKHRS { get; set; }        
        [Display(Name = "Deduct Break Hours From Total Work Hours")]
        public bool SHIFT_FLAG_ADD_BREAK { get; set; }
        [Display(Name = "Weekend different timings")]
        public bool SHIFT_WEEKEND_DIFF_TIME { get; set; }
        [Display(Name = "Week end Shift Start")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_START { get; set; }
        [Display(Name = "Week end Shift End")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_END { get; set; }
        [Display(Name = "Week end Break Start")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_BREAK_START { get; set; }
        [Display(Name = "Week end Break End")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_WEEKEND_BREAK_END { get; set; }
        [Required(ErrorMessage = "Please Provide Shift Early Search Hours")]
        [Display(Name = "Shift Early Search Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_EARLY_SEARCH_HRS { get; set; }
        [Required(ErrorMessage = "Please Provide Shift Late Search Hours")]
        [Display(Name = "Shift Late Search Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_LATE_SEARCH_HRS { get; set; }        
        public string SHIFT_CREATEDBY { get; set; }        
        public string SHIFT_MODIFIEDBY { get; set; }
        public bool SHIFT_ISDELETED { get; set; }        
        public string SHIFT_DELETEDBY { get; set; }
        [Required(ErrorMessage = "Please provide Company")]
        [Display(Name = "Company")]

        public string ipaddress { get; set; }

        public int COMPANY_ID { get; set; }
    }
}
