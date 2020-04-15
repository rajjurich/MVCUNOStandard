using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class ShiftDto
    {
        public int SHIFT_ID { get; set; }
        public string SHIFT_CODE { get; set; }
        public string SHIFT_DESCRIPTION { get; set; }
        public string SHIFT_TYPE { get; set; }
        public TimeSpan? SHIFT_START { get; set; }
        public TimeSpan? SHIFT_END { get; set; }
        public TimeSpan? SHIFT_BREAK_START { get; set; }
        public TimeSpan? SHIFT_BREAK_END { get; set; }
        public TimeSpan? SHIFT_BREAK_HRS { get; set; }
        public TimeSpan? SHIFT_WORKHRS { get; set; }
        public TimeSpan? SHIFT_HALFDAYWORKHRS { get; set; }        
    }
}