namespace Junior.Core.Extension
{
    public static class IntExtension
    {
        /// <summary>
        /// 判断是否合规端口
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsPort(this int param)
        {
            if (param >= 1 && param <= 65535)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规正整数和零
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsIntNotNegative(this int param)
        {
            if (param >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规月份数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsMonth(this int param)
        {
            if (param >= 1 && param <= 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否合规正整数
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool IsIntPositive(this int param)
        {
            if (param > 0)
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
