using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class HolidayVm
    {
        public string ipaddress { get; set; }
        public int HOLIDAY_ID { get; set; }

        [Display(Name = "Holiday Code")]
        [MaxLength(30)]
        [StringLength(30)]
        [Required(ErrorMessage = "Please Enter Holiday Code")]
        public virtual string HOLIDAY_CODE { get; set; }

        [Display(Name = "Holiday Description")]
        [MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "Please Enter Holiday Description")]
        public virtual string HOLIDAY_DESCRIPTION { get; set; }

        [Display(Name = "Holiday Type")]
        [MaxLength(4)]
        [StringLength(4)]
        [Required(ErrorMessage = "Please Select Holiday Type")]
        public virtual string HOLIDAY_TYPE { get; set; }

        [Display(Name = "Holiday Date")]
        [Required(ErrorMessage = "Please Select Holiday Date")]

        public virtual DateTime HOLIDAY_DATE { get; set; }

        [Display(Name = "Holiday Swipe Date ")]
        public virtual DateTime? HOLIDAY_SWAP { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please Select Company Name")]
        public int COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please Select Proper Option ")]
        public string LocationWise { get; set; }

        public virtual string ACTIVE_USER { get; set; }
        public List<HolidayLocVM> HolidayLoc { get; set; }

    }
    public class HolidayLocVM
    {
        public int HOLIDAYLOC_ID { get; set; }
        public bool IS_bool_HOLIDAYLOC_ID { get; set; }
        public int HOLIDAY_ID { get; set; }
        public int? HOLIDAY_LOC_ID { get; set; }
        public int? IS_OPTIONAL { get; set; }
        public bool IS_bool_OPTIONAL { get; set; }
        public int? IS_SYNC { get; set; }
        public int? COMPANY_ID { get; set; }
        public string OCE_ID { get; set; }
        public string OCE_DESCRIPTION { get; set; }

    }



}
