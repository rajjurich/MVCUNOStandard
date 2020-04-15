using System;
using System.Configuration;

namespace DataAccessHandler
{
    internal static class ConfigurationSettings
    {
        #region Properties

        public static string DefaultConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            }
        }

        public static string ProviderName
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DefaultConnection].ProviderName;
            }
        }

        public static string ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings[DefaultConnection].ConnectionString;
                }
                catch (Exception)
                {

                    throw new Exception(string.Format("Connection string '{0}' not found.", DefaultConnection));
                }
            }
        }

        #endregion

        #region Static Methods

        public static string GetConnectionString(string connectionName)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            }
            catch (Exception)
            {

                throw new Exception(string.Format("Connection string '{0}' not found.", connectionName));
            }
        }

        public static string GetProviderName(string connectionName)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[connectionName].ProviderName;
            }
            catch (Exception)
            {

                throw new Exception(string.Format("Connection string '{0}' not found.", connectionName));
            }
        }

        #endregion
    }
}
