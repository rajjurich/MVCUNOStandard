using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class AcsReader
    {
        [Key]
        public int RowID { get; set; }
        [Display(Name = "Reader ID")]
        [Required(ErrorMessage = "Please enter Reader Id")]
        public int READER_ID { get; set; }
        [Display(Name = "Reader Description")]
        [Required(ErrorMessage = "Please Provide Reader Description")]
        [StringLength(100,ErrorMessage="Maximum Length 100")]
        public string READER_DESCRIPTION { get; set; }
        [Required(ErrorMessage = "Please Provide Controller Id")]
        public int CTLR_ID { get; set; }
        [Display(Name = "Reader Mode")]
        [Required(ErrorMessage = "Please Provide Reader Mode")]
        [StringLength(2, ErrorMessage = "Reader Mode Maximum Length 2")]
        public string READER_MODE { get; set; }
        [Display(Name = "Reader Type")]
        [Required(ErrorMessage = "Please Provide Reader Type")]
        [StringLength(20, ErrorMessage = "Reader Type Maximum Length 20")]
        public string READER_TYPE { get; set; }
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
