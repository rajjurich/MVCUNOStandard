using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Services
{
    public static class DataConversion
    {

        public static Int32 Int32DataType(object objData)
        {
            return objData == DBNull.Value ? 0 : Convert.ToInt32(objData);

        }
        public static Int64 Int64DataType(object objData)
        {
            return objData == DBNull.Value ? 0 : Convert.ToInt64(objData);

        }
        public static Int16 ShortDataType(object objData)
        {
            return objData == DBNull.Value ? (Int16)0 : Convert.ToBoolean(objData) ? (Int16)1 : (Int16)0;

        }

        public static string StringDataType(object objData)
        {
            return objData == DBNull.Value ? "" : Convert.ToString(objData);

        }

    }
}