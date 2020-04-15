using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace UNO.AppModel
{
    public  class ROLEMASTERCREATEMODEL
    {

        public int rolemasterid { get; set; }
        [Required(ErrorMessage="Role Name Required")]
        public string ROLE_NAME { get; set; }

        public int COMPANY_ID { get; set; }
        [Required(ErrorMessage="Please Select Company Name")]
        public string Company_name { get; set; }

    }
}
