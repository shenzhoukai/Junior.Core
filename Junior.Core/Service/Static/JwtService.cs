using Junior.Core.Extension;
using Junior.Core.Global;
using Junior.Core.ServiceModel.JwtToken;

namespace Junior.Core.Service.Static
{
    public static class JwtService
    {
        /// <summary>
        /// 生成JWT的头部
        /// </summary>
        /// <param name="strLoginType"></param>
        /// <param name="createDateTime"></param>
        /// <returns></returns>
        public static JwtHeader MakeJwtHeader(string strLoginType, long createDateTime)
        {
            JwtHeader jwtHeader = new JwtHeader();
            jwtHeader.type = Const.JwtType;
            jwtHeader.algo = Const.JwtAlgo;
            jwtHeader.created = createDateTime;
            jwtHeader.expire = MakeJwtExpire(strLoginType);
            return jwtHeader;
        }
        /// <summary>
        /// 生成JWT的载荷
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="strUserName"></param>
        /// <param name="strUniqID"></param>
        /// <returns></returns>
        public static JwtPayload MakeJwtPayload(int userID, string strUserName, string strUniqID)
        {
            JwtPayload jwtPayload = new JwtPayload();
            jwtPayload.UserID = userID;
            jwtPayload.UserName = strUserName;
            jwtPayload.UniqID = strUniqID;
            /* 可根据业务特性进行修改 */
            return jwtPayload;
        }
        /// <summary>
        /// 生成JWT的签名
        /// </summary>
        /// <param name="jwtHeader"></param>
        /// <param name="jwtPayload"></param>
        /// <returns></returns>
        public static JwtSignature MakeJwtSignature(JwtHeader jwtHeader, JwtPayload jwtPayload)
        {
            string strEncodedString = $"{CryptoService.EncodeBase64(jwtHeader.ToSafeJson())}.{CryptoService.EncodeBase64(jwtPayload.ToSafeJson())}";
            string strSignature = CryptoService.EncodeHMACSHA256(strEncodedString, ConfigService.GetValue("JwtSecret"));
            JwtSignature jwtSignature = new JwtSignature();
            jwtSignature.Signature = strSignature;
            return jwtSignature;
        }
        /// <summary>
        /// 使用JWT的三要素生成JwtToken
        /// </summary>
        /// <param name="jwtHeader"></param>
        /// <param name="jwtPayload"></param>
        /// <param name="jwtSignature"></param>
        /// <returns></returns>
        public static string MakeJwtToken(JwtHeader jwtHeader, JwtPayload jwtPayload, JwtSignature jwtSignature)
        {
            return $"{CryptoService.EncodeBase64(jwtHeader.ToSafeJson())}.{CryptoService.EncodeBase64(jwtPayload.ToSafeJson())}.{jwtSignature.Signature}";
        }
        /// <summary>
        /// 根据当前时间计算生成JWT有效期
        /// </summary>
        /// <returns></returns>
        public static long MakeJwtExpire(string strLoginType = "Web")
        {
            return DateTime.Now.AddMinutes(int.Parse(ConfigService.GetValue($"Jwt{strLoginType}ExpireTime"))).ToTimeStamp();
        }
        /// <summary>
        /// 使用用户信息的三要素生成JwtToken，附带访问标识
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="strUserName"></param>
        /// <param name="strUniqID"></param>
        /// <param name="strLoginType"></param>
        /// <returns></returns>
        public static string MakeJwtToken(int userID, string strUserName, string strUniqID, string strLoginType, long createDateTime)
        {
            JwtHeader jwtHeader = MakeJwtHeader(strLoginType, createDateTime);
            JwtPayload jwtPayload = MakeJwtPayload(userID, strUserName, strUniqID);
            JwtSignature jwtSignature = MakeJwtSignature(jwtHeader, jwtPayload);
            return MakeJwtToken(jwtHeader, jwtPayload, jwtSignature);
        }
        /// <summary>
        /// 解析JWT的载荷
        /// </summary>
        /// <param name="strJwtToken"></param>
        /// <returns></returns>
        public static JwtPayload DecodeJwtPayload(string strJwtToken)
        {
            string[] arrJwtToken = strJwtToken.Split('.');
            string strPayload = CryptoService.DecodeBase64(arrJwtToken[1]);
            return strPayload.FromJson<JwtPayload>();
        }
        /// <summary>
        /// 解析JWT的头部
        /// </summary>
        /// <param name="strJwtToken"></param>
        /// <returns></returns>
        public static JwtHeader DecodeJwtHeader(string strJwtToken)
        {
            string[] arrJwtToken = strJwtToken.Split('.');
            string strHeader = CryptoService.DecodeBase64(arrJwtToken[0]);
            return strHeader.FromJson<JwtHeader>();
        }
        /// <summary>
        /// 校验JWT
        /// </summary>
        /// <param name="strJwtToken"></param>
        /// <param name="strUniqID"></param>
        /// <param name="strLoginType"></param>
        /// <returns></returns>
        public static bool VerifyJwtToken(string strJwtToken, string strUniqID, string strLoginType = "Web")
        {
            bool value = false;
            try
            {
                JwtHeader jwtHeader = DecodeJwtHeader(strJwtToken);
                if (jwtHeader.type != Const.JwtType || jwtHeader.algo != Const.JwtAlgo || DateTimeService.TsNow() > jwtHeader.expire)
                    return value;
                JwtPayload jwtPayload = DecodeJwtPayload(strJwtToken);
                if(!string.IsNullOrEmpty(strUniqID))
                {
                    if (jwtPayload.UniqID != strUniqID)
                        return value;
                }
                string strNewJwtToken = MakeJwtToken(jwtPayload.UserID, jwtPayload.UserName, jwtPayload.UniqID, strLoginType, jwtHeader.created);
                if (strNewJwtToken == strJwtToken)
                    value = true;
            }catch{}
            return value;
        }
    }
}
