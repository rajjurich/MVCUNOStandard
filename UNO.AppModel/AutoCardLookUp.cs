using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{

    public class AutoCardLookUp
    {
        [Display(Name="Event Date")]
        public DateTime Event_DateTime { get; set; }
        [Display(Name = "Event card code")]
        public string Event_Card_Code { get; set; }
        [Display(Name = "Employee Name")]
        public string Emp_Name { get; set; }
        [Display(Name = "Select Criteria")]
        public string CTLR_READER_ID { get; set; }
        [Display(Name = "Card Status")]
        public string STATUS { get; set; }
        public string READER_DESCRIPTION { get; set; }

    }

    //public class CtrlRdrList
    //{
    //    public string CTLR_READER_ID { get; set; }
    //    public string READER_DESCRIPTION { get; set; }
    //}
}
