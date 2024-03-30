using Junior.Core.Service.Static;
using System.IO;

namespace Junior.Core.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 字符串转日期格式
        /// </summary>
        /// <param name="strDt"></param>
        /// <returns></returns>
        public static DateTime ToDt(this string strDt)
        {
            strDt = strDt.Replace("-0", "/");
            strDt = strDt.Replace("-", "/");
            return DateTime.Parse(strDt);
        }
        /// <summary>
        /// 字符串日期转时间戳
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this string strDateTime)
        {
            long timeStamp = 946656000;
            try
            {
                timeStamp = (Convert.ToDateTime(strDateTime).Ticks - TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local).Ticks) / 10000000;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return timeStamp;
        }
        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsNull(this string strParam)
        {
            return string.IsNullOrEmpty(strParam);
        }
        /// <summary>
        /// 判断是否Base64字符串
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsBase64String(this string strParam)
        {
            if (strParam.IsNull())
                return false;
            bool value = false;
            string strDecode = string.Empty;
            try
            {
                strDecode = CryptoService.DecodeBase64(strParam);
                if (!strDecode.IsNull())
                {
                    value = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return value;
        }
        /// <summary>
        /// 判断是否合规日期时间字符串
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string strParam)
        {
            if (strParam.IsNull())
                return false;
            return RegexService.IsMatch(strParam, @"^[1-9]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1])\s+(20|21|22|23|[0-1]\d):[0-5]\d:[0-5]\d$");
        }
        /// <summary>
        /// 判断是否合规日期字符串
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsDate(this string strParam)
        {
            if (strParam.IsNull())
                return false;
            return RegexService.IsMatch(strParam, @"^((([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29))$");
        }
        /// <summary>
        /// 判断是否合规IP地址或域名
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsIPorDomain(this string strParam)
        {
            if (strParam.IsNull())
                return false;
            bool value = IsIPv4(strParam);
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
        /// 判断是否合规SteamID
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsSteamID(this string strParam)
        {
            if (strParam.IsNull())
                return false;
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
        /// 判断是否合规用户名，大小写字母和数字
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsUserName(this string strParam)
        {
            if (strParam.IsNull())
                return false;
            return IsCharacterAndNumber(strParam);
        }
        /// <summary>
        /// 判断是否版本号
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsVersion(this string strParam)
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
        /// 判断是否合规真名
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static bool IsTrueName(this string strParam)
        {
            if (strParam.IsNull())
                return false;
            if (strParam.Length >= 2)
            {
                return IsChineseTxt(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否短信验证码
        /// </summary>
        /// <param name="strParam"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsSmsCode(this string strParam, int length = 6)
        {
            if (strParam.IsNull())
                return false;
            if (strParam.Length == length)
            {
                return IsNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否邮件验证码
        /// </summary>
        /// <param name="strParam"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsMailCode(this string strParam, int length = 6)
        {
            if (strParam.IsNull())
                return false;
            if (strParam.Length == length)
            {
                return IsNumber(strParam);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检测是否IP地址
        /// </summary>
        /// <param name="strIP"></param>
        /// <returns></returns>
        public static bool IsIPv4(this string strIP)
        {
            if (strIP.IsNull())
                return false;
            if (strIP.Length < 7 || strIP.Length > 15)
                return false;
            return RegexService.IsMatch(strIP, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        }
        /// <summary>
        /// 检测是否手机号
        /// </summary>
        /// <param name="strMobilePhone"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(this string strMobilePhone)
        {
            if (strMobilePhone.IsNull())
                return false;
            return RegexService.IsMatch(strMobilePhone, @"^1[3456789]\d{9}$");
        }
        /// <summary>
        /// 检测是否身份证号码
        /// </summary>
        /// <param name="strCardNum"></param>
        /// <returns></returns>
        public static bool IsIdCardNum(this string strCardNum)
        {
            if (strCardNum.IsNull())
                return false;
            return RegexService.IsMatch(strCardNum, @"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$");
        }
        /// <summary>
        /// 检测是否Email
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public static bool IsEmail(this string strEmail)
        {
            if (strEmail.IsNull())
                return false;
            return RegexService.IsMatch(strEmail, @"^([a-zA-Z0-9._-])+@([a-zA-Z0-9_-])+(\.[a-zA-Z0-9_-])+");
        }
        /// <summary>
        /// 检测是否大小写字母和数字
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsCharacterAndNumber(this string strRaw)
        {
            if (strRaw.IsNull())
                return false;
            return RegexService.IsMatch(strRaw, @"^[A-Za-z0-9]+$");
        }
        /// <summary>
        /// 检测是否中文
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsChineseTxt(this string strRaw)
        {
            if (strRaw.IsNull())
                return false;
            return RegexService.IsMatch(strRaw, @"[\u4e00-\u9fa5]");
        }
        /// <summary>
        /// 检测是否数字
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool IsNumber(this string strRaw)
        {
            if (strRaw.IsNull())
                return false;
            return RegexService.IsMatch(strRaw, @"^(-)?[0-9]*$");
        }
        /// <summary>
        /// 判断是否银行卡号
        /// </summary>
        /// <param name="strCardNum"></param>
        /// <returns></returns>
        public static bool IsBankCardNum(this string strCardNum)
        {
            if (strCardNum.IsNull())
                return false;
            bool value = IsNumber(strCardNum);
            if(value)
            {
                value = long.TryParse(strCardNum, out long cardNum);
                if(value)
                {
                    value = cardNum.IsBankCardNum();
                }
            }
            return value;
        }
        /// <summary>
        /// 检测是否包含空格
        /// </summary>
        /// <param name="strRaw"></param>
        /// <returns></returns>
        public static bool ContainsSpace(this string strRaw)
        {
            if (strRaw.Contains(" "))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ContainsFrontEndSpace(this string strRaw)
        {
            if (strRaw != strRaw.Trim())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
