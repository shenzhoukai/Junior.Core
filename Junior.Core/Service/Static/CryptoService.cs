using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Junior.Core.Service.Static
{
    public static class CryptoService
    {
        /// <summary>
        /// 生成当前的SaasToken
        /// </summary>
        /// <param name="strSaasKey"></param>
        /// <returns></returns>
        public static string EncodeLatestSaasToken(string strSaasKey)
        {
            string strRaw = string.Format("{0}{1}{0}", DateTime.Now.ToString("yyyyMMddHHmm"), strSaasKey);
            string strTrueToken = EncodeMD5(EncodeMD5(EncodeMD5(strRaw)));
            return strTrueToken;
        }
        /// <summary>
        /// 生成上一分钟的SaasToken
        /// </summary>
        /// <param name="strSaasKey"></param>
        /// <returns></returns>
        public static string EncodeLastSaasToken(string strSaasKey)
        {
            string strRaw = string.Format("{0}{1}{0}", DateTime.Now.AddMinutes(-1).ToString("yyyyMMddHHmm"), strSaasKey);
            string strTrueToken = EncodeMD5(EncodeMD5(EncodeMD5(strRaw)));
            return strTrueToken;
        }
        /// <summary>
        /// 订单ID算法
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strExtraData"></param>
        /// <returns></returns>
        public static string EncodeOrderID(string strType, string strExtraData = "")
        {
            Guid guid = Assembly.GetExecutingAssembly().ManifestModule.ModuleVersionId;
            return strType + strExtraData + EncodeMD5(DateTimeService.Now()).ToUpper();
        }
        /// <summary>
        /// 唯一识别ID加密算法
        /// </summary>
        /// <param name="numID"></param>
        /// <param name="strString"></param>
        /// <returns></returns>
        public static string EncodeUniqID(int numID, string strString)
        {
            string strUserIDHashed = EncodeMD5(numID.ToString());
            string strUserNameHashed = EncodeMD5(strString);
            string strDatetimeHashed = EncodeMD5(DateTimeService.Now());
            string value = EncodeMD5(EncodeBase64(strUserIDHashed + strUserNameHashed + strDatetimeHashed));
            return value;
        }
        /// <summary>
        /// 用户密码加密算法，使用基础加密算法
        /// </summary>
        /// <param name="strPwd">输入未加密的密码</param>
        /// <returns></returns>
        public static string EncodeUserPwd(string strPwd)
        {
            string value = EncodeMD5(EncodeBase64(EncodeMD5(strPwd)));
            return value;
        }
        /// <summary>
        /// 基础加密算法，先MD5再Base64再MD5
        /// </summary>
        /// <param name="strData">输入未加密的数据</param>
        /// <returns></returns>
        public static string EncodeBasicCrypto(string strData)
        {
            string value = EncodeMD5(EncodeBase64(EncodeMD5(strData)));
            return value;
        }
        /// <summary>
        /// 不安全加密算法，3次Base64
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string EncodeUnsafeCrypto(string strData)
        {
            string value = EncodeBase64(EncodeBase64(EncodeBase64(strData)));
            return value;
        }
        public static string DecodeUnsafeCrypto(string strData)
        {
            string value = DecodeBase64(DecodeBase64(DecodeBase64(strData)));
            return value;
        }
        /// <summary>
        /// MD5加密，输出32位小写
        /// </summary>
        /// <param name="strData">输入字符串</param>
        /// <returns></returns>
        public static string EncodeMD5(string strData)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = Encoding.UTF8.GetBytes(strData);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="strData">输入字符串</param>
        /// <returns></returns>
        public static string EncodeBase64(string strData)
        {
            byte[] arrData = Encoding.UTF8.GetBytes(strData);
            string strBase64 = Convert.ToBase64String(arrData);
            return strBase64;
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="strData">输入字符串</param>
        /// <returns></returns>
        public static string DecodeBase64(string strData)
        {
            string strRaw = string.Empty;
            try
            {
                byte[] bytes = Convert.FromBase64String(strData);
                strRaw = Encoding.UTF8.GetString(bytes);
            }
            catch { }
            return strRaw;
        }
        /// <summary>
        /// HMACSHA256加密算法
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="strSecret"></param>
        /// <returns></returns>
        public static string EncodeHMACSHA256(string strData, string strSecret)
        {
            strSecret = strSecret ?? "";
            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(strSecret);
            byte[] messageBytes = encoding.GetBytes(strData);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        /// <summary>
        /// 生成短链接
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string EncodeShortUrl(string strKey, int len = 6)
        {
            string value = string.Empty;
            List<string> listFilter = new List<string>();
            listFilter.Add("o");
            listFilter.Add("0");
            listFilter.Add("O");
            listFilter.Add("I");
            listFilter.Add("l");
            listFilter.Add("i");
            listFilter.Add("1");
            for (int i = 0; i < len; i++)
            {
                string strChar = EncodeMD5(strKey + Guid.NewGuid().ToString()).ToUpper().Substring(i, 1);
                while (!ValidService.CharIsAllow(listFilter, strChar))
                {
                    strChar = EncodeMD5(strKey + Guid.NewGuid().ToString()).ToUpper().Substring(i, 1);
                }
                value += strChar;
            }
            return value;
        }
        /// <summary>
        /// 生成外部订单号
        /// </summary>
        /// <param name="strPrefix"></param>
        /// <returns></returns>
        public static string EncodeOutTradeNo(string strPrefix)
        {
            return strPrefix + EncodeUniqID(9999, Guid.NewGuid().ToString()).ToUpper();
        }
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string EncodeRSA(string text, byte[] publicKey)
        {
            using (var x509 = new X509Certificate2(publicKey))
            {
                using (var rsa = (RSACryptoServiceProvider)x509.PublicKey.Key)
                {
                    var buff = rsa.Encrypt(Encoding.UTF8.GetBytes(text), true);
                    return Convert.ToBase64String(buff);
                }
            }
        }
        /// <summary>
        /// 计算文件的 MD5 值
        /// </summary>
        /// <param name="fileName">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string EncodeMd5FileHash(string fileName)
        {
            return HashFile(fileName, "md5");
        }
        /// <summary>
        /// 计算文件的哈希值
        /// </summary>
        /// <param name="fileName">要计算哈希值的文件名和路径</param>
        /// <param name="algName">算法:sha1,md5</param>
        /// <returns>哈希值16进制字符串</returns>
        private static string HashFile(string fileName, string algName)
        {
            if (!File.Exists(fileName))
                return string.Empty;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] hashBytes = HashData(fs, algName);
            fs.Close();
            return ConvertService.ByteArrayToHexString(hashBytes);
        }
        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="stream">要计算哈希值的 Stream</param>
        /// <param name="algName">算法:sha1,md5</param>
        /// <returns>哈希值字节数组</returns>
        private static byte[] HashData(Stream stream, string algName)
        {
            HashAlgorithm algorithm;
            if (algName == null)
            {
                throw new ArgumentNullException("algName 不能为 null");
            }
            if (string.Compare(algName, "sha1", true) == 0)
            {
                algorithm = SHA1.Create();
            }
            else
            {
                if (string.Compare(algName, "md5", true) != 0)
                {
                    throw new Exception("algName 只能使用 sha1 或 md5");
                }
                algorithm = MD5.Create();
            }
            return algorithm.ComputeHash(stream);
        }
    }
}
