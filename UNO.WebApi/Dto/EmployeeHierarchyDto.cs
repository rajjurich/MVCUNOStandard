using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class EmployeeHierarchyDto
    {
        public int ENT_HierarchyDef_ID { get; set; }
        public string EmployeeCode { get; set; }      
        public string EmployeeName { get; set; }        
        public string ReportingName { get; set; }
        public string ReportingCode { get; set; }  
        public string Designation { get; set; }
    }
}