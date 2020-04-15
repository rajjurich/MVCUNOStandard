using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class EntOrgCommonTypes
    {
        public virtual int COMMON_TYPES_ID { get; set; }
        public virtual string COMMON_TYPES { get; set; }
        public virtual bool IS_SYNC { get; set; }
        public virtual int COMPANY_ID { get; set; }
    }
}