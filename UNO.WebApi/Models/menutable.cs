using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class menutable
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public int RelationsId { get; set; }
    }
}