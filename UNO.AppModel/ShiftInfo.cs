using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class ShiftInfo
    {
        [Key]
        public int SHIFT_ID { get; set; }        
        [Display(Name = "Shift ID")]
        public string SHIFT_CODE { get; set; }        
        [Display(Name = "Shift Description")]
        public string SHIFT_DESCRIPTION { get; set; }        
        [Display(Name = "Shift Type")]
        public string SHIFT_TYPE { get; set; }        
        [Display(Name = "Shift Start")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_START { get; set; }        
        [Display(Name = "Shift End")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_END { get; set; }        
        [Display(Name = "Break Start")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_BREAK_START { get; set; }        
        [Display(Name = "Break End")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_BREAK_END { get; set; }        
        [Display(Name = "Total Break Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_BREAK_HRS { get; set; }        
        [Display(Name = "Total Work Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_WORKHRS { get; set; }        
        [Display(Name = "Halfday Work Hours")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> SHIFT_HALFDAYWORKHRS { get; set; }        
        
    }
}
