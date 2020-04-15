using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class CommonMaster
    {
        public int COMMON_TYPES_ID { get; set; }
        public string COMMON_NAME { get; set; }
    }

    public class EntityType
    {
        public int ENTITY_TYPE_ID { get; set; }
        public string ENTITY_TYPE { get; set; }
    }
}