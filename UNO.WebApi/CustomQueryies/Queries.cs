using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.CustomQueryies
{
    public static class Queries
    {
        public static string RoleDataAccessQuery(int id)
        {
            return " SELECT com.COMPANY_ID FROM ent_company com" +
                   " join ENT_ROLE_DATA_ACCESS da on com.COMPANY_ID=da.MAPPED_ENTITY_ID" +
                   " join ENT_ORG_COMMON_TYPES ct on ct.COMMON_TYPES_ID=da.COMMON_TYPES_ID and ct.COMMON_TYPES='COMPANY'" +
                   " WHERE COMPANY_ISDELETED =0 and da.USER_CODE=" + id;
        }
    }
}