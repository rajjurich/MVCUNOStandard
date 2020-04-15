using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class ModuleMenuApi
    {
        public int ModuleMenuId { get; set; }

        public string ModuleMenuName { get; set; }

        public List<SubMenuApi> SubMesnus { get; set; }
    }
}
