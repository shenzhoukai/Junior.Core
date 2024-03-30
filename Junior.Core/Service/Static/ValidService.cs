using System.Text.RegularExpressions;

namespace Junior.Core.Service.Static
{
    public static class ValidService
    {
        /// <summary>
        /// 检测是否Null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNull(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断正则表达式
        /// </summary>
        /// <param name="strRaw"></param>
        /// <param name="strFormat"></param>
        /// <returns></returns>
        public static bool RegexIsMatch(string strRaw, string strFormat)
        {
            Regex regex = new Regex(strFormat, RegexOptions.None);
            return regex.IsMatch(strRaw);
        }
        /// <summary>
        /// 检测是否IP地址
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static bool IsIPAddress(string strIP)
        {
            if (string.IsNullOrEmpty(strIP) || strIP.Length < 7 || strIP.Length > 15) return false;
            return RegexIsMatch(strIP, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        }
        /// <summary>
        /// 检测是否手机号
        /// </summary>
        /// <param name="strMobilePhone"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string strMobilePhone)
        {
            return RegexIsMatch(strMobilePhone, @"^1[3456789]\d{9}$");
        }
        /// <summary>
        /// 检测是否身份证号码
        /// </summary>
        /// <param name="strCardNum"></param>
        /// <returns></returns>
        public static bool IsIdCardNum(string strCardNum)
        {
            return RegexIsMatch(strCardNum, @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$");
        }
        /// <summary>
        /// 检测是否Email
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public static bool IsEmail(string strEmail)
        {
            return RegexIsMatch(strEmail, @"^([a-zA-Z0-9._-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+");
        }
        /// <summary>
        /// 检测是否大小写字母和数字
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsCharacterAndNumber(string strRaw)
        {
            return RegexIsMatch(strRaw, @"^[A-Za-z0-9]+$");
        }
        /// <summary>
        /// 检测是否Md5
        /// </summary>
        /// <param name="strMd5"></param>
        /// <returns></returns>
        public static bool IsMd5(string strMd5)
        {
            if (strMd5.Length == 32 || strMd5.Length == 16)
            {
                return IsCharacterAndNumber(strMd5);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检测是否中文
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsChineseTxt(string strRaw)
        {
            return RegexIsMatch(strRaw, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        /// 检测是否数字
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsNumber(string strRaw)
        {
            return RegexIsMatch(strRaw, @"^(-)?[0-9]*$");
        }
        /// <summary>
        /// 检测是否金钱
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static bool IsMoney(decimal raw)
        {
            return RegexIsMatch(raw.ToString(), @"(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)");
        }
        /// <summary>
        /// 检测是否数字
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static bool IsInt(int raw)
        {
            return RegexIsMatch(raw.ToString(), @"^(-)?[0-9]*$");
        }
        /// <summary>
        /// 检测是否日期
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsDate(string strRaw)
        {
            return RegexIsMatch(strRaw, @"^((([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29))$");
        }
        /// <summary>
        /// 检测是否日期时间
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsDateTime(string strRaw)
        {
            return RegexIsMatch(strRaw, @"^[1-9]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])\s+(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d$");
        }
        /// <summary>
        /// 检测是否包含空格
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool ContainsSpace(string strRaw)
        {
            bool value = false;
            if (strRaw.Contains(" "))
            {
                value = true;
            }
            return value;
        }
        public static bool ContainsFrontEndSpace(string strRaw)
        {
            bool value = false;
            if (strRaw != strRaw.Trim())
            {
                value = true;
            }
            return value;
        }
    }
}
