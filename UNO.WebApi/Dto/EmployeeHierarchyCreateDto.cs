using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class EmployeeHierarchyCreateDto
    {
        public int Hier_Mgr_ID { get; set; }        
        public List<EmployeeDto> Employees { get; set; }

        public string ipaddress { get; set; }
    }
}