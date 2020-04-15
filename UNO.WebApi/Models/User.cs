using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class User
    {
        public string ipaddress { get; set; }
        public virtual int USER_ID { get; set; }
        public virtual string USER_CODE { get; set; }
        public virtual string Password { get; set; }
        public virtual int ROLE_ID { get; set; }
        public virtual string ROLE_NAME { get; set; }
        public virtual bool EssEnabled { get; set; }
        public virtual bool PASSWORD_RESET { get; set; }
        public virtual int COMPANY_ID { get; set; }
        public virtual string COMPANY_NAME { get; set; }
        public virtual string ACTIVE_USER { get; set; }

        public string sendbadrequest { get; set; }

    }
}