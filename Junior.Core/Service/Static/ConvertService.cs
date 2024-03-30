using System.Reflection;

namespace Junior.Core.Service.Static
{
    public static class ConvertService
    {
        /// <summary>
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        public static string ByteArrayToHexString(byte[] buf)
        {
            string returnStr = string.Empty;
            if (buf != null)
            {
                for (int i = 0; i < buf.Length; i++)
                {
                    returnStr += buf[i].ToString("X2");
                }
            }
            return returnStr;
        }
        /// <summary>
        /// 类转换成IDic
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ObjToDictionary(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (PropertyInfo property in properties)
            {
                if (!property.CanRead || !property.CanWrite)
                    continue;
                object value = property.GetValue(obj);
                string key = property.Name;
                string strValue = value?.ToString();
                dict[key] = strValue ?? string.Empty;
            }
            return dict;
        }
    }
}
