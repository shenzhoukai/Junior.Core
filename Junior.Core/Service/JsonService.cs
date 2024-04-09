using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Junior.Core.Service
{
    public class JsonService
    {
        /// <summary>
        /// 从Json字符串的对象中获取值
        /// </summary>
        /// <param name="strJson"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetValue(string strJson, string strKey)
        {
            JObject jo = (JObject)JsonConvert.DeserializeObject(strJson);
            return jo[strKey].ToString();
        }
        /// <summary>
        /// 从Json字符串的数组中获取值
        /// </summary>
        /// <param name="strJson"></param>
        /// <param name="index"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetValue(string strJson, int index, string strKey)
        {
            JArray ja = (JArray)JsonConvert.DeserializeObject(strJson);
            return ja[index][strKey].ToString();
        }
    }
}
