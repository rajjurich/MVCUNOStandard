using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class LocateCardHolder
    {

        public string EVENT_EMPLOYEE_CODE { get; set; }
        public string EVENT_CARD_CODE { get; set; }
        public string EMP_NAME { get; set; }
        public string EPD_MARITAL_STATUS { get; set; }
        public string EPD_PHONE_ONE { get; set; }
        public string READER_DESCRIPTION { get; set; }
    }
}