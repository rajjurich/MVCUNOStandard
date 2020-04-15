using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class SubMenuApi
    {
        public int SubMenuId { get; set; }

        public string SubMenuName { get; set; }

        public List<menutableapi> MenuNames { get; set; }
    }
}
