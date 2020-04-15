using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UNO.AppModel
{
    public class RoleModule
    {
        [Key]
        public int RowID { get; set; }
        public long ROLE_ID { get; set; }
        public string ROLE_Name { get; set; }
        public virtual int COMPANY_ID { get; set; }

    }
}
