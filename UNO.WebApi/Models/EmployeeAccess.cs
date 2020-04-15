using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UNO.WebApi.Dto;

namespace UNO.WebApi.Models
{
    public class EmployeeAccess
    {
        public int COMMON_TYPES_ID { get; set; }
        public virtual ICollection<int> EntityIds { get; set; }
        public ICollection<long> AL_IDs { get; set; }

        public string ipaddress { get; set; }
    }
}