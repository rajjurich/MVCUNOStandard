using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UNO.AppModel
{
    [Checkboxvalidation(ErrorMessage = "Please select anyone checkbox")]
    public class Checkboxlistfingure
    {
        
        public Boolean Enroll { get; set; }
        
        public Boolean Time_Attandance { get; set; }
    }
}
