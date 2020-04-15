using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class Role
    {
        public virtual int ROLE_ID { get; set; }
        public virtual string ROLE_NAME { get; set; }
        public virtual int COMPANY_ID { get; set; }
        public virtual string UserID { get; set; }        

    }
}