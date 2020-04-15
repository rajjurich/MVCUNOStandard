using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EalConfigView
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
