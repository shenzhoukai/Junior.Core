using Junior.Core.Extension;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Junior.Core.Service.Static
{
    public class RedisService
    {
        private ConnectionMultiplexer redis = null;
        private IDatabase database;
        /// <summary>
        /// 初始化链接Redis
        /// </summary>
        public RedisService()
        {
            string strRedisUrl = $"{ConfigService.GetValue("RedisHost")}:{ConfigService.GetValue("RedisPort")}";
            string strRedisPwd = ConfigService.GetValue("RedisPwd");
            if(strRedisPwd.IsNull())
            {
                redis = ConnectionMultiplexer.Connect(strRedisUrl);
            }
            else
            {
                redis = ConnectionMultiplexer.Connect($"{strRedisUrl},password={strRedisPwd}");
            }
            bool tmp = int.TryParse(ConfigService.GetValue("RedisDbIndex"), out int dbIndex);
            if(tmp)
                dbIndex = -1;
            database = redis.GetDatabase(dbIndex);
        }
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public string GetValue(string strKey)
        {
            return database.StringGet(strKey);
        }
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        public void SetValue(string strKey, string strValue)
        {
            database.StringSet(strKey, strValue);
        }
        /// <summary>
        /// 删除键值
        /// </summary>
        /// <param name="strKey"></param>
        public void DeleteKey(string strKey)
        {
            database.KeyDelete(strKey);
        }
        /// <summary>
        /// 设置Key的过期时间
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="sec"></param>
        public void SetKeyExpire(string strKey, int sec)
        {
            database.KeyExpire(strKey, TimeSpan.FromSeconds(sec));
        }
        /// <summary>
        /// 设置hashset
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="hashEntries"></param>
        public void SetHash(string strKey, HashEntry[] hashEntries)
        {
            database.HashSet(strKey, hashEntries);
        }
    }
}
