namespace Junior.Core.Service
{
    public class ValidService
    {
        /// <summary>
        /// 验证字符是否被允许
        /// </summary>
        /// <param name="listFilter"></param>
        /// <param name="strChar"></param>
        /// <returns></returns>
        public static bool CharIsAllow(List<string> listFilter, string strChar)
        {
            bool charAllow = false;
            foreach (string strFilter in listFilter)
            {
                if (charAllow)
                {
                    continue;
                }
                else
                {
                    if (strChar != strFilter)
                    {
                        charAllow = true;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return charAllow;
        }
    }
}
