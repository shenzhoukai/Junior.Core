using Junior.Core.Extension;
using System.Data;

namespace Junior.Core.Service.Static
{
    public static class MySqlService
    {
        /// <summary>
        /// MySql是否开启
        /// </summary>
        /// <returns></returns>
        private static bool IsEnable()
        {
            string strMySqlEnable = ConfigService.GetValue("MySqlEnable");
            bool tmp = bool.TryParse(strMySqlEnable, out bool isEnable);
            if (tmp)
            {
                return isEnable;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// MySql连接字符串
        /// </summary>
        /// <returns></returns>
        private static string MySqlConnString()
        {
            return $"server={ConfigService.GetValue("MySqlHost")};database={ConfigService.GetValue("MySqlDb")};user={ConfigService.GetValue("MySqlUser")};password={ConfigService.GetValue("MySqlPwd")};";
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql, string strConn = "")
        {
            DataSet dataSet = new DataSet();
            if (!IsEnable())
                return dataSet;
            if (strSql.IsNull())
                return dataSet;
            if (strConn.IsNull())
            {
                strConn = MySqlConnString();
            }
            return SqlService.Query(strSql, strConn);
        }
    }
}
