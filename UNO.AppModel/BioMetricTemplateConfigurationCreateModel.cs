using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UNO.AppModel
{
    public class BioMetricTemplateConfigurationCreateModel
    {
        public int bio_metric_id { get; set; }
        [Required(ErrorMessage = "FingureForEnroll should Be matched with count")]
        public string FingureForEnroll { get; set; }
        [Required(ErrorMessage = "FingureForAttandance should Be equal to 2")]
        public string FingureForAttandance { get; set; }

        
        public Boolean Enroll { get; set; }
        public string ipaddress { get; set; }
        
        public Boolean Time_Attandance { get; set; }

        public string COMPANY_ID { get; set; }

        public string CompanyName { get; set; }


        [Required(ErrorMessage="Count Required")]
        [Range(2, 10, ErrorMessage = "Minimum 2 To 10 Count")]
        public int Mycount { get; set; }

        


    }
}
