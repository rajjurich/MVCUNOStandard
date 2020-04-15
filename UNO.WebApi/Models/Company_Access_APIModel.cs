using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Company_Access_APIModel
    {
        public int Companyid { get; set; }

        public string CompanyName { get; set; }


        public List<ModuleMenu> ModuleSeleted { get; set; }

        public List<int> Menulistfromweb { get; set; }

        public List<int> RelationIdfromweb { get; set; }

        public List<string> IsStatus { get; set; }
    }
}