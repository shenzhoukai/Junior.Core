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
        /// <summary>
        /// 判断是否银行卡号
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        public static bool IsBankCardNum(this long cardNum)
        {
            string strCardNum = cardNum.ToString();
            if(strCardNum.Length >= 16 && strCardNum.Length <= 19)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
    }
}
