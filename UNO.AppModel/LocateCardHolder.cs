using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
  
    public class LocateCardHolder
    {
        [Display(Name="Employee Code")]
        public string EVENT_EMPLOYEE_CODE { get; set; }
        [Display(Name = "Event Card Code")]
        public string EVENT_CARD_CODE { get; set; }
        [Display(Name = "Employee Name")]
        public string EMP_NAME { get; set; }
        [Display(Name = "Marital Status")]
        public string EPD_MARITAL_STATUS { get; set; }
        [Display(Name = "Phone Number")]
        public string EPD_PHONE_ONE { get; set; }
        [Display(Name = "Reader Description")]
        public string READER_DESCRIPTION { get; set; }

        [Display(Name = "Select Criteria")]
        public int FILTER_CRITERIA { get; set; }
        [Display(Name = "Employee/Card Code")]
        public string FILTER { get; set; }
    }
}
