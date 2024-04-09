using Junior.Core.Service;

namespace Junior.Core.Extension
{
    public static class DecimalExtension
    {
        /// <summary>
        /// 检测是否金钱
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static bool IsMoney(this decimal raw)
        {
            return RegexService.IsMatch(raw.ToString(), @"(^[1-9]([0-9]+)?(\.[0-9]{1,2})?$)|(^(0){1}$)|(^[0-9]\.[0-9]([0-9])?$)");
        }
    }
}
