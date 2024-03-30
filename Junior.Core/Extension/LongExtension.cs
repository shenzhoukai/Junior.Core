namespace Junior.Core.Extension
{
    public static class LongExtension
    {
        /// <summary>
        /// 时间戳转DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timeStamp)
        {
            try
            {
                return DateTimeOffset.FromUnixTimeSeconds(timeStamp).LocalDateTime;
            }
            catch
            {
                return new DateTime(2000, 1, 1, 0, 0, 0);
            }
        }
    }
}
