using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class AccessPointDetails
    {
        [Required(ErrorMessage = "AP Id Required")]
        public decimal AP_ID { get; set; }
        [Required(ErrorMessage = "Controller Id Required")]
        public int CTLR_ID { get; set; }
        [Required(ErrorMessage = "Reader Id Required")]
        public int READER_ID { get; set; }
        [Required(ErrorMessage = "Door Id Required")]
        public int DOOR_ID { get; set; }
        [Required(ErrorMessage="Door Type Required")]
        [StringLength(50, ErrorMessage = "Door Type Maximum Length 50")]    
        public string DOOR_TYPE { get; set; }
        [Required(ErrorMessage = "Please Provide Door Open Duration")]
        [StringLength(10, ErrorMessage = "Door Open Duration Maximum Length 10")]        
        public string DOOR_OPEN_DURATION { get; set; }
        [Required(ErrorMessage = "Please Provide Door Open Duration")]
        [StringLength(10, ErrorMessage = "Door Feedback Duration Maximum Length 10")] 
        public string DOOR_FEEDBACK_DURATION { get; set; }
    }
}
