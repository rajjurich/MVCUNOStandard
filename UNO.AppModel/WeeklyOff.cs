using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class WeeklyOff
    {
        [Display(Name = "Week Off Code")]
        [Required(ErrorMessage = "Please Enter Week Off Code")]
        [StringLength(30, ErrorMessage = "Week Off Code Length 30")]       
        public string MWK_DESC { get; set; }
        public int MWK_CD { get; set; }

        [Display(Name = "Week Day")]
        [Required(ErrorMessage = "Please Select Week Day")]
        public int MWK_DAY { get; set; }

        public string ipaddress { get; set; }
        public int MWK_OFF { get; set; }
        public string MWK_PAT { get; set; }
        public bool FSTMWK { get; set; }
        public bool SECMWK { get; set; }
        public bool THDMWK { get; set; }
        public bool FURMWK { get; set; }
        public bool FIFMWK { get; set; }
        public string MWK_DAY_NAME { get; set; }
        public string MWK_OFF_NAME { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please Select Company Name")]
        public int COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }

    }
}
