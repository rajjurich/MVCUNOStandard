using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EmployeeAddress
    {
        [Key]
        public int EMPLOYEE_ADDRESS { get; set; }
        public Nullable<int> EMPLOYEE_ID { get; set; }
        public string EAD_ADDRESS_TYPE { get; set; }
        public string EAD_ADDRESS { get; set; }
        public string EAD_CITY { get; set; }
        public string EAD_PIN { get; set; }
        public string EAD_STATE { get; set; }
        public string EAD_COUNTRY { get; set; }
        public string EAD_PHONE_ONE { get; set; }
        public string EAD_PHONE_TWO { get; set; }
        public Nullable<bool> EAD_ISDELETED { get; set; }
        public Nullable<System.DateTime> EAD_DELETEDDATE { get; set; }
        public bool IS_SYNC { get; set; }
    }
}