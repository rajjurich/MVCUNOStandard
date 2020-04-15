using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class CategoryDto
    {
        
        public int CATEGORY_ID { get; set; }        
        public int ORG_CATEGORY_ID { get; set; }
        public string OCE_DESCRIPTION { get; set; }
        public TimeSpan? EARLY_GOING { get; set; }       
        public TimeSpan? LATE_COMING { get; set; }       
        public TimeSpan? EXHRS_BEFORE_SHIFT_HRS { get; set; }       
        public TimeSpan? EXHRS_AFTER_SHIFT_HRS { get; set; }        
        public string COMPENSATORYOFF_CODE { get; set; }
        public int COMPANY_ID { get; set; }
    }
}