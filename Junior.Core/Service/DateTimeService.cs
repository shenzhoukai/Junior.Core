using Junior.Core.Extension;
using Junior.Core.Global;

namespace Junior.Core.Service
{
    public class DateTimeService
    {
        /// <summary>
        /// 获取当前日期的字符串
        /// </summary>
        /// <returns></returns>
        public static string Now()
        {
            return DateTime.Now.ToString(Const.DateTimeFormat);
        }
        /// <summary>
        /// 获取当前日期的字符串
        /// </summary>
        /// <returns></returns>
        public static string Today()
        {
            return DateTime.Now.ToDateString();
        }
        /// <summary>
        /// 获取当前日期的Dt
        /// </summary>
        /// <returns></returns>
        public static DateTime DtNow()
        {
            return DateTime.Now;
        }
        /// <summary>
        /// 获取当前日期的时间戳
        /// </summary>
        /// <returns></returns>
        public static long TsNow()
        {
            return DateTime.Now.ToTimeStamp();
        }
        /// <summary>
        /// 时间戳转日期格式
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime TimeStampToDateTime(string timeStamp)
        {
            try
            {
                DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dateTimeStart.Add(toNow);
            }
            catch
            {
                return "2000-01-01 00:00:00".ToDt();
            }
        }
        /// <summary>
        /// 2000年的日期格式
        /// </summary>
        /// <returns></returns>
        public static DateTime Dt2000()
        {
            return "2000-01-01 00:00:00".ToDt();
        }
        /// <summary>
        /// 获取当前日期的年份，0000
        /// </summary>
        /// <returns></returns>
        public static int ThisYear()
        {
            return DateTime.Now.Year;
        }
        /// <summary>
        /// 获取当前日期的月份，1-12
        /// </summary>
        /// <returns></returns>
        public static int ThisMonth()
        {
            return DateTime.Now.Month;
        }
        /// <summary>
        /// 获取当前日期的天数，1-31
        /// </summary>
        /// <returns></returns>
        public static int ThisDay()
        {
            return DateTime.Now.Day;
        }
        /// <summary>
        /// 获取当前日期的年份，0000
        /// </summary>
        /// <returns></returns>
        public static string ThisYearString()
        {
            return DateTime.Now.Year.ToString();
        }
        /// <summary>
        /// 获取当前日期的月份，00
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ThisMonthString(bool prefix = false)
        {
            int mon = DateTime.Now.Month;
            string value = mon.ToString();
            if (prefix)
            {
                if (mon < 10)
                {
                    return "0" + value;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
        /// <summary>
        /// 获取当前日期的上个月，00
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string LastMonthString(bool prefix = false)
        {
            int mon = DateTime.Now.Month - 1;
            if (mon == 0)
            {
                mon = 12;
            }
            string value = mon.ToString();
            if (prefix)
            {
                if (mon < 10)
                {
                    return "0" + value;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
        /// <summary>
        /// 获取当前日期的天数，00
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static string ThisDayString(bool prefix = false)
        {
            int day = DateTime.Now.Day;
            string value = day.ToString();
            if (prefix)
            {
                if (day < 10)
                {
                    return "0" + value;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }
        /// <summary>
        /// 判断日期是否是今年
        /// </summary>
        /// <param name="strDt"></param>
        /// <returns></returns>
        public static bool InThisYear(string strDt)
        {
            if (strDt.Substring(0, 4) == ThisYearString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断日期是否是这个半年
        /// </summary>
        /// <param name="strDt"></param>
        /// <returns></returns>
        public static bool InThisHalfYear(string strDt)
        {
            if (strDt.Substring(0, 4) == ThisYearString())
            {
                string strMon = strDt.Substring(5, 2);
                if (strMon.Substring(0, 1) == "0")
                {
                    strMon = strMon.Substring(1, 1);
                }
                int mon = int.Parse(strMon);
                int thisMon = ThisMonth();
                if (thisMon <= 6)
                {
                    if (mon <= 6)
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
                    if (mon >= 7)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断日期是否是本月
        /// </summary>
        /// <param name="strDt"></param>
        /// <returns></returns>
        public static bool InThisMonth(string strDt)
        {
            if (strDt.Substring(5, 2) == ThisMonthString(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断日期是否是上个月
        /// </summary>
        /// <param name="strDt"></param>
        /// <returns></returns>
        public static bool InLastMonth(string strDt)
        {
            if (strDt.Substring(5, 2) == LastMonthString(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断日期是否是今天
        /// </summary>
        /// <param name="strDt"></param>
        /// <returns></returns>
        public static bool IsToday(string strDt)
        {
            if (strDt == Today())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static TimeSpan DiffTimeSpan(DateTime dtStart, DateTime dtEnd)
        {
            TimeSpan ts = new TimeSpan();
            ts = dtEnd - dtStart;
            return ts;
        }
        /// <summary>
        /// 计算时间差，天数
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static double DiffDays(DateTime dtStart, DateTime dtEnd)
        {
            double days = 0;
            TimeSpan ts = DiffTimeSpan(dtStart, dtEnd);
            days = ts.TotalDays;
            return days;
        }
        /// <summary>
        /// 计算时间差，小时数
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static double DiffHrs(DateTime dtStart, DateTime dtEnd)
        {
            double hrs = 0;
            TimeSpan ts = DiffTimeSpan(dtStart, dtEnd);
            hrs = ts.TotalHours;
            return hrs;
        }
        /// <summary>
        /// 计算时间差，分钟数
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static double DiffMins(DateTime dtStart, DateTime dtEnd)
        {
            double mins = 0;
            TimeSpan ts = DiffTimeSpan(dtStart, dtEnd);
            mins = ts.TotalMinutes;
            return mins;
        }
        /// <summary>
        /// 计算时间差，秒数
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static double DiffSecs(DateTime dtStart, DateTime dtEnd)
        {
            double secs = 0;
            TimeSpan ts = DiffTimeSpan(dtStart, dtEnd);
            secs = ts.TotalSeconds;
            return secs;
        }
        /// <summary>
        /// 计算时间差，毫秒数
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static double DiffMs(DateTime dtStart, DateTime dtEnd)
        {
            double ms = 0;
            TimeSpan ts = DiffTimeSpan(dtStart, dtEnd);
            ms = ts.TotalMilliseconds;
            return ms;
        }
        /// <summary>
        /// 判断时间是否在范围内
        /// </summary>
        /// <param name="myDt"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public static bool DateInRange(DateTime myDt, DateTime dtStart, DateTime dtEnd)
        {
            bool value = false;
            double diff1 = DiffSecs(dtStart, myDt);
            if (diff1 >= 0)
            {
                double diff2 = DiffSecs(myDt, dtEnd);
                if (diff2 >= 0)
                {
                    value = true;
                }
            }
            return value;
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string TimeStamp(int length = 10)
        {
            string value = string.Empty;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            switch (length)
            {
                default:
                case 10:
                    value = Convert.ToInt64(ts.TotalSeconds).ToString();
                    break;
                case 13:
                    value = Convert.ToInt64(ts.TotalMilliseconds).ToString();
                    break;
            }
            return value;
        }
    }
}
