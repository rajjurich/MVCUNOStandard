using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class EmployeeCardConfigCreateDto
    {
        public int USECOUNT { get; set; }

        public DateTime ACTIVATION_DATE { get; set; }

        public DateTime EXPIRY_DATE { get; set; }

        public List<EmployeeDtoforConfig> Employees { get; set; }

        public Boolean Remember { get; set; }

        public string ipaddress { get; set; }
    }
}