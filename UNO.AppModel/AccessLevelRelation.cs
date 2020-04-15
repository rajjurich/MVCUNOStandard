using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class AccessLevelRelation
    {     
        [Required]
        public string AL_ENTITY_TYPE { get; set; }        
        public List<int> AccessLevelReaders { get; set; }    
    }
}
