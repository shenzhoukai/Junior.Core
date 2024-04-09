using System.Configuration;

namespace Junior.Core.Service
{
    public class ConfigService
    {
        /// <summary>
        /// 获取Config的值
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetValue(string strKey)
        {
            return ConfigurationManager.AppSettings[strKey];
        }
        /// <summary>
        /// 设置Config的值
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public static void SetValue(string strKey, string strValue)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings[strKey].Value = strValue;
            cfg.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
