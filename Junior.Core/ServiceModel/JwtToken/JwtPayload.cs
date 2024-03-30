namespace Junior.Core.ServiceModel.JwtToken
{
    public class JwtPayload
    {
        /// <summary>
        /// User表的主键
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 用户的用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户的唯一识别ID
        /// </summary>
        public string UniqID { get; set; }
    }
}
