using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Junior.Core.Service.Static
{
    public static class JsonService
    {
        /// <summary>
        /// 实体对象转换为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 对象转安全Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToSafeJson(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
        }
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
