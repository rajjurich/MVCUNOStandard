using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class ModuleMenu
    {
        public int ModuleMenuId { get; set; }

        public string ModuleMenuName { get; set; }

        public List<SubMenu> SubMesnus { get; set; }
    }
}