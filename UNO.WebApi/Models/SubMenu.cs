using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class SubMenu
    {
        public int SubMenuId { get; set; }

        public string SubMenuName { get; set; }

        public List<menutable> MenuNames { get; set; }
    }
}