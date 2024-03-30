using Newtonsoft.Json;

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
        public static string ToSafeJson(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj).Replace("\"", "\\\"");
        }
        /// <summary>
        /// Json字符串转换为实体对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="strJson">Json字符串</param>
        /// <returns></returns>
        public static T FromJson<T>(this string strJson)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(strJson);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
