using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class EalConfigDto
    {
        [Required]
        public string ENTITY_ID { get; set; }
        [Required]
        public string Entity_Name { get; set; }
        [Required]
        public string Level_Description { get; set; }
        [Required]
        public string Al_from { get; set; }
    }
}