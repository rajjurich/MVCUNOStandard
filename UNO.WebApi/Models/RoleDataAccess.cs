using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class RoleDataAccess
    {
        public int DATA_ACCESS_ID { get; set; }
        public Nullable<int> USER_CODE { get; set; }
        public Nullable<int> COMMON_TYPES_ID { get; set; }
        public Nullable<int> MAPPED_ENTITY_ID { get; set; }
        public bool IS_SYNC { get; set; }
    }
}