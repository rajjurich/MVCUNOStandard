using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class ShiftPatternDto
    {
        public int SHIFT_PATTERN_ID { get; set; }
        public string SHIFT_PATTERN_CODE { get; set; }
        public string SHIFT_PATTERN_DESCRIPTION { get; set; }
        public string SHIFT_PATTERN_TYPE { get; set; }
        public string SHIFT_PATTERN { get; set; }
    }
}