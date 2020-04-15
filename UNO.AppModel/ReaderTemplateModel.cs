using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
  public class ReaderTemplateModel
    {
        [Key]
        public int RowID { get; set; }
        public int COMPANY_ID { get; set; }
        [Display(Name = "Select Controller")]
        [Required(ErrorMessage = "Please Select Controller")]
        public int ControllerID { get; set; }

        [Display(Name = "Select Event")]
        [Required(ErrorMessage = "Please Select Event")]
        public int EventID { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please Enter Description")]
        [StringLength(100, ErrorMessage = "Description Maximum Length 100")]       
        public string EventMessage { get; set; }
        public string ControllerName { get; set; }
        public string EventName { get; set; }

        public string ipaddress { get; set; }

    }
}
