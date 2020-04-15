using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class Zone
    {
        [Key]
        public int ZONE_ID { get; set; }
        [Display(Name = "Zone Description")]
        [Required(ErrorMessage = "Please Provide Zone Description")]
        [StringLength(100, ErrorMessage = "Maximum Length 50")]
        public string ZONE_DESCRIPTION { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Please Provide Company")]
        public int? COMPANY_ID { get; set; }
        public string ZONE_DELETEDBY { get; set; }
        public string ZONE_CREATEDBY { get; set; }        
        public string ZONE_MODIFIEDBY { get; set; }

        public string ipaddress { get; set; }
        public List<AcsReaderInfo> acsReaderInfos { get; set; }
    }
}
