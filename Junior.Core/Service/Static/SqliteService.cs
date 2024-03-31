using Junior.Core.Extension;
using System.Data;

namespace Junior.Core.Service.Static
{
    public static class SqliteService
    {
        /// <summary>
        /// Sqlite是否开启
        /// </summary>
        /// <returns></returns>
        private static bool IsEnable()
        {
            string strSqliteEnable = ConfigService.GetValue("SqliteEnable");
            bool tmp = bool.TryParse(strSqliteEnable, out bool isEnable);
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
        /// Sqlite连接字符串
        /// </summary>
        /// <returns></returns>
        private static string SqliteConnString()
        {
            string strConn = $"Data Source={ConfigService.GetValue("SqliteFilePath")};Version=3;";
            if (!ConfigService.GetValue("SqlitePwd").IsNull())
                strConn += $"Password={ConfigService.GetValue("SqlitePwd")};";
            return strConn;
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
                strConn = SqliteConnString();
            }
            return SqlService.Query(strSql, strConn);
        }
    }
}
