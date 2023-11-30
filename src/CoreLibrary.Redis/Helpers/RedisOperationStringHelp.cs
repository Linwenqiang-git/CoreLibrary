using StackExchange.Redis;
using CoreLibrary.Core;
using System.Text;

namespace CoreLibrary.Redis
{
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 保存字符串
        /// </summary>
        public async Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default,
            bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StringSetAsync(key, value, expiry);
        }

        /// <summary>
        /// 保存字符串 当不存在的时候保存
        /// </summary>
        public async Task<bool> StringSetNotExistsAsync(string key, string value, TimeSpan? expiry = default,
            bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StringSetAsync(key, value, expiry, When.NotExists);
        }

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = default,
            bool isContainsRedisPrefix = true)
        {
            if (value == null)
                throw new ArgumentException(nameof(value));
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            string val = string.Empty;
            if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid))
            {
                val = value.ToStr();
            }
            else
            {
                val = await value.ToJsonAsync();
            }

            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StringSetAsync(key, val, expiry);
        }

        /// <summary>
        /// 保存集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<bool> StringSetAsync<T>(string key, List<T> value, TimeSpan? expiry = default,
            bool isContainsRedisPrefix = true)
        {
            if (value == null || value.Count() <= 0)
                throw new ArgumentException(nameof(value));
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            List<T> li = new List<T>();
            foreach (var item in value)
            {
                li.Add(item);
            }

            var val = await li.ToJsonAsync();
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StringSetAsync(key, val, expiry);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        public async Task<string> StringGetAsync(string key, bool isContainsRedisPrefix = true)
        {
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StringGetAsync(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<T> StringGetAsync<T>(string key, bool isContainsRedisPrefix = true)
        {
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            var value = await _redisConnection.Database.StringGetAsync(key);
            if (value.ToString() == null)
            {
                return default;
            }

            return await value.ToStr().JsonToAsync<T>();
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <typeparam name="T"></typeparam>
        public async Task<object> StringGetObjAsync(string key, bool isContainsRedisPrefix = true)
        {
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            var value = await _redisConnection.Database.StringGetAsync(key);
            if (value.ToString() == null)
            {
                return default;
            }

            return value.ToString();
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<List<T>?> StringGetAsync<T>(List<string> keys, bool isContainsRedisPrefix = true)
        {
            if (keys == null || keys.Count <= 0)
            {
                return default;
            }

            await _redisConnection.CreateConnectionAsync();
            //拼接key
            var stringKeys = keys.Select(item =>
                new RedisKey(GetRedisKey(item, Enums.EKeyOperator.String, isContainsRedisPrefix))).ToArray();
            //获取值
            var data = await _redisConnection.Database.StringGetAsync(stringKeys);
            if (data == null || data.Length <= 0)
            {
                return default;
            }

            //拼接json
            var stringBuilder = new StringBuilder();
            var result = new List<T>();
            stringBuilder.Append("[");
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString().TrimStart('[').TrimEnd(']'));
                if (i < data.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append("]");
            return await stringBuilder.ToString().JsonToAsync<List<T>>();
        }

        /// <summary>
        /// 自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> StringIncrementAsync(string key, long value = 1, bool isContainsRedisPrefix = true)
        {
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StringIncrementAsync(key, value);
        }

        /// <summary>
        /// lua脚本
        /// </summary>
        private static string IncrLimitValue =
            EmbeddedResourceLoader.GetEmbeddedResource("CoreLibrary.Redis.Lua.IncrLimitValue.lua");

        /// <summary>
        /// 自增 当达到limit将移除(返回0)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="limit">递增的最大数 当达到执行数将移除(返回0)</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> StringIncrementLimitRemoveAsync(string key, long limit, long value = 1,
            bool isContainsRedisPrefix = true)
        {
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            var res = await _redisConnection.Database.ScriptEvaluateAsync(IncrLimitValue, new RedisKey[]
            {
                key
            }, new RedisValue[]
            {
                value,
                limit
            });
            return res.ToLong();
        }

        /// <summary>
        /// 递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> StringDecrementAsync(string key, long value = 1, bool isContainsRedisPrefix = true)
        {
            key = GetRedisKey(key, Enums.EKeyOperator.String, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StringDecrementAsync(key, value);
        }
    }
}