using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class RoleDataAccess
    {        
        public int DATA_ACCESS_ID { get; set; }
        [Display(Name = "User Code")]
        [Required(ErrorMessage="Please Select User")]
        public Nullable<int> USER_CODE { get; set; }        
        [Display(Name = "Mapped Entity Ids")]
        public List<MappedEntityId> MappedEntityIds { get; set; }

        public string ipaddress { get; set; }
    }

    public class MappedEntityId
    {
        public string CommonTypes { get; set; }
        public Nullable<int> MAPPED_ENTITY_ID { get; set; }  
    }
}
