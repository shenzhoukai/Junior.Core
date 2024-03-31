using Junior.Core.Global;

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
                timeStamp = (dateTime.Ticks - TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1), TimeZoneInfo.Local).Ticks) / 10000000;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return timeStamp;
        }
        /// <summary>
        /// 日期转换为日期时间字符串，yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDtString(this DateTime dt)
        {
            return dt.ToString(Const.DateTimeFormat);
        }
        /// <summary>
        /// 日期转换为日期字符串，yyyy-MM-dd
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime dt)
        {
            return dt.ToString(Const.DateFormat);
        }
    }
}
