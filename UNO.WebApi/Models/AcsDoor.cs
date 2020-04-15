using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AcsDoor
    {
        public int DOOR_ID { get; set; }
        public string DOOR_DESCRIPTION { get; set; }
        public int CTLR_ID { get; set; }         
        public string DOOR_TYPE { get; set; }        
        public string DOOR_OPEN_DURATION { get; set; }        
        public string DOOR_FEEDBACK_DURATION { get; set; }
        public bool DOOR_ISDELETED { get; set; }
        public Nullable<System.DateTime> DOOR_DELETEDDATE { get; set; }
        public int READER_ID { get; set; }
        public bool AP_FLAG { get; set; }
    }
}