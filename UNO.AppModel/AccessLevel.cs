using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class AccessLevel
    {
        [Key]
        public long AL_ID { get; set; }
        [Required(ErrorMessage = "Please Provide Decription")]
        [Display(Name = "Description")]
        public string AL_DESCRIPTION { get; set; }
        [Required(ErrorMessage = "Please Select Time Zone")]
        [Display(Name = "TimeZone")]
        public long AL_TIMEZONE_ID { get; set; }
        [Required(ErrorMessage="Please Select Readers")]
        public AccessLevelRelation AccessLevelRelation { get; set; }

        public string ipaddress { get; set; }
        
    }
    public class AccessLevelInfo
    {       
        public long AL_ID { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string AL_DESCRIPTION { get; set; }
    }
}
