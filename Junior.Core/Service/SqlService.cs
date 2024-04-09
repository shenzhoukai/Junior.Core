using Junior.Core.Extension;
using System.Data;
using System.Data.SqlClient;

namespace Junior.Core.Service
{
    public class SqlService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="strConn"></param>
        /// <returns></returns>
        public static DataSet Query(string strSql, string strConn)
        {
            DataSet dataSet = new DataSet();
            if (strSql.IsNull())
                return dataSet;
            if (strConn.IsNull())
                return dataSet;
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(strSql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            adapter.Fill(dataSet);
                        }
                    }
                }
                connection.Close();
            }
            return dataSet;
        }
    }
}
