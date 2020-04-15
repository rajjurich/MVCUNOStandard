using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AccessLevel
    {
        [Key]
        public long AL_ID { get; set; }
        [Required]
        public string AL_DESCRIPTION { get; set; }
        [Required]
        public long AL_TIMEZONE_ID { get; set; }
        public AccessLevelRelationInfo AccessLevelRelation { get; set; }
        public string ipaddress { get; set; }

    }
}