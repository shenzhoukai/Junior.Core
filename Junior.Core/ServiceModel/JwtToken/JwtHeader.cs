namespace Junior.Core.ServiceModel.JwtToken
{
    public class JwtHeader
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 算法
        /// </summary>
        public string algo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public long created { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public long expire { get; set; }
    }
}
