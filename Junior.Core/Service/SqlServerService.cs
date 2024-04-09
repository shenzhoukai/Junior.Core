using Junior.Core.Extension;
using System.Data;

namespace Junior.Core.Service
{
    public class SqlServerService
    {
        /// <summary>
        /// SqlServer是否开启
        /// </summary>
        /// <returns></returns>
        private static bool IsEnable()
        {
            string strSqlServerEnable = ConfigService.GetValue("SqlServerEnable");
            bool tmp = bool.TryParse(strSqlServerEnable, out bool isEnable);
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
        /// SqlServer连接字符串
        /// </summary>
        /// <returns></returns>
        private static string SqlServerConnString()
        {
            return $"Data Source={ConfigService.GetValue("SqlServerHost")};Initial Catalog={ConfigService.GetValue("SqlServerInitialCatalog")};User Id={ConfigService.GetValue("SqlServerUserID")};password={ConfigService.GetValue("SqlServerPwd")};";
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
                strConn = SqlServerConnString();
            }
            return SqlService.Query(strSql, strConn);
        }
    }
}
