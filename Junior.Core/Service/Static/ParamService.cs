using System.Reflection;

namespace Junior.Core.Service.Static
{
    public class ParamService
    {
        /// <summary>
        /// 类转换成IDic
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ConvertToDictionary(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanRead || !property.CanWrite) continue; // Skip non-readable or non-writeable properties

                object value = property.GetValue(obj);
                string key = property.Name;
                string strValue = value?.ToString();

                dict[key] = strValue ?? "";
            }

            return dict;
        }
        /// <summary>
        /// 判断是否必填参数
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsRequiredParam(string strParam)
        {
            bool value = false;
            if (ValidService.IsNull(strParam))
                return value;
            value = true;
            return value;
        }
        /// <summary>
        /// 判断是否选填参数
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsOptionalParam(string strParam)
        {
            return true;
        }
        /// <summary>
        /// 判断是否合规用户名，大小写字母和数字
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsUserName(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsCharacterAndNumber(strParam);
        }
        /// <summary>
        /// 判断是否版本号
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsVersion(string strParam)
        {
            bool value = false;
            if (strParam.Contains("."))
            {
                string[] arrVer = strParam.Split('.');
                if (arrVer.Length > 1)
                {
                    value = true;
                    foreach (string strVerNum in arrVer)
                    {
                        value = value & int.TryParse(strVerNum, out int verNum);
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// 判断是否Base64字符串
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsBase64String(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            string strDecode = string.Empty;
            try
            {
                strDecode = CryptoService.DecodeBase64(strParam);
                if (!ValidService.IsNull(strDecode))
                {
                    value = true;
                }
            }
            catch
            {
            }
            return value;
        }
        /// <summary>
        /// 判断是否合规邮箱
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsEmail(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsEmail(strParam);
        }
        /// <summary>
        /// 判断是否合规手机号
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsMobilePhone(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsMobilePhone(strParam);
        }
        /// <summary>
        /// 判断是否合规Md5
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsMd5(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsMd5(strParam);
        }
        /// <summary>
        /// 判断是否合规真名
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsTrueName(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length >= 2)
            {
                return ValidService.IsChineseTxt(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规身份证号码
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsCardNum(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsIdCardNum(strParam);
        }
        /// <summary>
        /// 判断是否合规QQ号
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsQQ(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsNumber(strParam);
        }
        /// <summary>
        /// 判断是否验证码
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsSmsCode(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length == 6)
            {
                return ValidService.IsNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否商店ID
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsShopUniqID(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length == 6)
            {
                return ValidService.IsCharacterAndNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否资源ID
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsAppUniqID(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length == 8)
            {
                return ValidService.IsCharacterAndNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否服务器ID
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsConnUniqID(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length == 7)
            {
                return ValidService.IsCharacterAndNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否聊天组ID
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsChatRoomUniqID(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length == 5)
            {
                return ValidService.IsCharacterAndNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否验证码
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsMailCode(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length == 6)
            {
                return ValidService.IsNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规日期时间字符串
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsDateTime(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsDateTime(strParam);
        }
        /// <summary>
        /// 判断是否合规日期字符串
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsDate(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsDate(strParam);
        }
        /// <summary>
        /// 判断是否合规IP地址
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsIP(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            return ValidService.IsIPAddress(strParam);
        }
        /// <summary>
        /// 判断是否合规IP地址或域名
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsIPorDomain(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            value = ValidService.IsIPAddress(strParam);
            if (!value)
            {
                if (strParam.Contains("."))
                {
                    value = true;
                }
            }
            return value;
        }
        /// <summary>
        /// 判断是否合规整数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckIsInt(int param)
        {
            return ValidService.IsInt(param);
        }
        /// <summary>
        /// 判断是否合规SteamID
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsSteamID(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length == 17)
            {
                if (strParam.Substring(0, 4) == "7656")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规SuitCode
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool CheckIsSuitCode(string strParam)
        {
            bool value = false;
            value = CheckIsRequiredParam(strParam);
            if (!value)
                return value;
            if (strParam.Length >= 1 && strParam.Length <= 3)
            {
                string[] arrSuitCode = { "S", "M", "L", "XL", "XXL" };
                if (arrSuitCode.Contains(strParam))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规正整数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckIsIntPositive(int param)
        {
            bool value = ValidService.IsInt(param);
            if (value)
            {
                if (param > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规正整数和零
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckIsIntNotNegative(int param)
        {
            bool value = ValidService.IsInt(param);
            if (value)
            {
                if (param >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规月份数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckIsMonth(int param)
        {
            bool value = ValidService.IsInt(param);
            if (value)
            {
                if (param >= 1 && param <= 12)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规端口
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckIsPort(int param)
        {
            bool value = ValidService.IsInt(param);
            if (value)
            {
                if (param >= 1 && param <= 65535)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规金钱
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckIsMoney(decimal param)
        {
            return ValidService.IsMoney(param);
        }
    }
}
