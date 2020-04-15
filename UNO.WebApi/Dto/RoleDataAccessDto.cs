using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Dto
{
    public class RoleDataAccessDto
    {
        [Key]
        public int DATA_ACCESS_ID { get; set; }       
        public int USER_CODE { get; set; }       
        public List<MappedEntityId> MappedEntityIds { get; set; }
        public string ipaddress { get; set; }
    }
    public class MappedEntityId
    {
        public string CommonTypes { get; set; }
        public string MAPPED_ENTITY_NAME { get; set; }
        public Nullable<int> MAPPED_ENTITY_ID { get; set; }
    }
}