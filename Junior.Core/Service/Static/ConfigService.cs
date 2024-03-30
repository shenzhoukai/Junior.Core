using System.Configuration;

namespace Junior.Core.Service.Static
{
    public static class ConfigService
    {
        public static string GetValue(string strKey)
        {
            return ConfigurationManager.AppSettings[strKey];
        }
    }
}
