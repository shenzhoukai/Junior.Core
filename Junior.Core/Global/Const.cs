namespace Junior.Core.Global
{
    public static class Const
    {
        /// <summary>
        /// 全局日期格式化，支持MySQL，格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 全局日期格式化，支持MySQL，格式：yyyy-MM-dd
        /// </summary>
        public static string DateFormat = "yyyy-MM-dd";
        /// <summary>
        /// JWT的类型
        /// </summary>
        public static string JwtType = "JWT";
        /// <summary>
        /// JWT的算法
        /// </summary>
        public static string JwtAlgo = "HS256";
    }
}
