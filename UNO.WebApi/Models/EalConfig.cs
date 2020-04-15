using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EalConfig
    {
        [Key]
        public long EAL_ID { get; set; }
        [Required]
        public int ENTITY_TYPE { get; set; }
        [Required]
        public string ENTITY_ID { get; set; }        
        public int EMPLOYEE_CODE { get; set; }
        public string CARD_CODE { get; set; }
        public long AL_ID { get; set; }
        [Required]
        public string FLAG { get; set; }
        public int CONTROLLER_ID { get; set; }                
    }    
}