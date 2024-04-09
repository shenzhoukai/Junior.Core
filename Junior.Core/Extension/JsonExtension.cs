using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Junior.Core.Extension
{
    public static class JsonExtension
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
    }
}
