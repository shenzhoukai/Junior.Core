namespace Junior.Core.Extension
{
    public static class StringExtension
    {
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
                timeStamp = (Convert.ToDateTime(strDateTime).Ticks - TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1),TimeZoneInfo.Local).Ticks) / 10000000;
            }
            catch
            { }
            return timeStamp;
        }
        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNull(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
