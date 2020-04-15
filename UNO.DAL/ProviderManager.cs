using System.Configuration;
using System.Data.Common;

namespace UNO.DAL
{
    public class ProviderManager
    {
        #region Property

        public string ProviderName { get; set; }

        public DbProviderFactory Factory
        {
            get
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(ProviderName);
                return factory;
            }
        }

        #endregion

        #region Constructor

        public ProviderManager()
        {
            ProviderName = ConfigurationSettings.GetProviderName(ConfigurationSettings.DefaultConnection);
        }

        public ProviderManager(string providerName)
        {
            ProviderName = providerName;
        }

        #endregion
    }
}
