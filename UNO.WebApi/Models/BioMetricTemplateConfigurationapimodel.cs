using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace UNO.WebApi.Models
{
    public class BioMetricTemplateConfigurationapimodel
    {
        public int bio_metric_id { get; set; }
        public string FingureForEnroll { get; set; }

        public string CompanyName { get; set; }
       
        public string FingureForAttandance { get; set; }


        public Boolean Enroll { get; set; }

        public Boolean Time_Attandance { get; set; }

        public string COMPANY_ID { get; set; }


        
        public int Mycount { get; set; }

        public string ipaddress { get; set; }
    }
}