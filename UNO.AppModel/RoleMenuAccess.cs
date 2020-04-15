using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class RoleMenuAccess
    {
        public long ROLE_ACCESS_ID { get; set; }
        public string ROLE_ID { get; set; }

        [Display(Name = "Menu Name")]
        public string MENU_ID { get; set; }
        public Nullable<short> CreateAccess { get; set; }
        public Nullable<short> ViewAccess { get; set; }
        public Nullable<short> UpdateAccess { get; set; }
        public Nullable<short> DeleteAccess { get; set; }
        public Nullable<short> IsDeleted { get; set; }
    }
}
