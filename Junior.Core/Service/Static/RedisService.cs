using Junior.Core.Extension;
using StackExchange.Redis;

namespace Junior.Core.Service.Static
{
    public static class RedisService
    {
        /// <summary>
        /// Redis是否开启
        /// </summary>
        /// <returns></returns>
        private static bool IsEnable()
        {
            string strRedisEnable = ConfigService.GetValue("RedisEnable");
            bool tmp = bool.TryParse(strRedisEnable, out bool isEnable);
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
        /// 生成Redis连接字符串
        /// </summary>
        /// <returns></returns>
        private static string RedisConnString()
        {
            string strRedisUrl = $"{ConfigService.GetValue("RedisHost")}:{ConfigService.GetValue("RedisPort")}";
            string strRedisPwd = ConfigService.GetValue("RedisPwd");
            if (strRedisPwd.IsNull())
            {
                return strRedisUrl;
            }
            else
            {
                return $"{strRedisUrl},password={strRedisPwd}";
            }
        }
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public static string GetValue(string strKey)
        {
            if (!IsEnable())
                return string.Empty;
            string strValue = string.Empty;
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnString()))
            {
                bool tmp = int.TryParse(ConfigService.GetValue("RedisDbIndex"), out int dbIndex);
                if (tmp)
                    dbIndex = -1;
                IDatabase database = redis.GetDatabase(dbIndex);
                //Redis操作 - 开始
                strValue = database.StringGet(strKey);
                //Redis操作 - 结束
                redis.Close();
            }
            return strValue;
        }
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public static void SetValue(string strKey, string strValue)
        {
            if (!IsEnable())
                return;
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnString()))
            {
                bool tmp = int.TryParse(ConfigService.GetValue("RedisDbIndex"), out int dbIndex);
                if (tmp)
                    dbIndex = -1;
                IDatabase database = redis.GetDatabase(dbIndex);
                //Redis操作 - 开始
                database.StringSet(strKey, strValue);
                //Redis操作 - 结束
                redis.Close();
            }
        }
        /// <summary>
        /// 删除键值
        /// </summary>
        /// <param name="strKey"></param>
        public static void DeleteKey(string strKey)
        {
            if (!IsEnable())
                return;
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnString()))
            {
                bool tmp = int.TryParse(ConfigService.GetValue("RedisDbIndex"), out int dbIndex);
                if (tmp)
                    dbIndex = -1;
                IDatabase database = redis.GetDatabase(dbIndex);
                //Redis操作 - 开始
                database.KeyDelete(strKey);
                //Redis操作 - 结束
                redis.Close();
            }
        }
        /// <summary>
        /// 设置Key的过期时间
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="sec"></param>
        public static void SetKeyExpire(string strKey, int sec)
        {
            if (!IsEnable())
                return;
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnString()))
            {
                bool tmp = int.TryParse(ConfigService.GetValue("RedisDbIndex"), out int dbIndex);
                if (tmp)
                    dbIndex = -1;
                IDatabase database = redis.GetDatabase(dbIndex);
                //Redis操作 - 开始
                database.KeyExpire(strKey, TimeSpan.FromSeconds(sec));
                //Redis操作 - 结束
                redis.Close();
            }
        }
        /// <summary>
        /// 设置hashset
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="hashEntries"></param>
        public static void SetHash(string strKey, HashEntry[] hashEntries)
        {
            if (!IsEnable())
                return;
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(RedisConnString()))
            {
                bool tmp = int.TryParse(ConfigService.GetValue("RedisDbIndex"), out int dbIndex);
                if (tmp)
                    dbIndex = -1;
                IDatabase database = redis.GetDatabase(dbIndex);
                //Redis操作 - 开始
                database.HashSet(strKey, hashEntries);
                //Redis操作 - 结束
                redis.Close();
            }
        }
    }
}
