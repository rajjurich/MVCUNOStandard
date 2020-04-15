using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UNO.AppModel
{
    public class Company_Level_Menu_Model
    {
        [Required(ErrorMessage="Company Should Selected")]
        public int Companyid { get; set; }

        public string CompanyName { get; set; }

        public List<ModuleMenuApi> ModuleSeleted { get; set; }

        

        public List<int> Menulistfromweb { get; set; }

        public List<int> RelationIdfromweb { get; set; }

        public List<string> IsStatus { get; set; }

        public string ipaddress { get; set; }

    }
}
