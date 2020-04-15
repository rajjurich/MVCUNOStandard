using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class UserLoginApiModel
    {
        public long USER_ID { get; set; }
        public string USER_CODE { get; set; }

        public string Password { get; set; }

        public int isReset { get; set; }

        public string verionsofwebapp { get; set; }
    }
}