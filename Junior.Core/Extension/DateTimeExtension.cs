namespace Junior.Core.Extension
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 日期转时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime dateTime)
        {
            long timeStamp = 946656000;
            try
            {
                timeStamp = (dateTime.Ticks - TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1),TimeZoneInfo.Local).Ticks) / 10000000;
            }
            catch
            { }
            return timeStamp;
        }
    }
}
