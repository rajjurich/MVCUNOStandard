using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class EmployeeCardConfig
    {
        [Display(Name = "Employee Code")]
        public int CC_EMP_ID { get; set; }
        
        public int EMP_ID { get; set; }
        [Display(Name = "Card Code")]
        public string CARD_CODE { get; set; }
        [Display(Name = "Pin")]
        public string PIN { get; set; }
        [Display(Name = "Use Count")]
        public int USE_COUNT { get; set; }
     
        public bool IGNORE_APB { get; set; }
        public bool STATUS { get; set; }
        public DateTime ACTIVATION_DATE { get; set; }
        public DateTime EXPIRY_DATE { get; set; }


        public List<EmployeeInfo> Employees { get; set; }
        public bool Employee { get; set; }
        public bool Company { get; set; }
        public bool Location { get; set; }
        public bool Division { get; set; }
        public bool Department { get; set; }
        public bool Category { get; set; }

        public Boolean Remember { get; set; }


        public string ipaddress { get; set; }
    }

}
