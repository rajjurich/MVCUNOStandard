using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AccessLevelRelation
    {
        public Nullable<long> AL_ID { get; set; }
        public int RD_ZN_ID { get; set; }
        public string AL_ENTITY_TYPE { get; set; }
        public int CONTROLLER_ID { get; set; }
        public string AccesLevelArray { get; set; }
        public Nullable<int> ZoneId { get; set; }
    }
    public class AccessLevelRelationInfo
    {
        public string AL_ENTITY_TYPE { get; set; }        
        public virtual ICollection<int> AccessLevelReaders { get; set; }
    }

    public class AccessLevelRelationEdit
    {
        public Nullable<long> AL_ID { get; set; }       
        public int CONTROLLER_ID { get; set; }       
        public Nullable<int> ZoneId { get; set; }
    }
}