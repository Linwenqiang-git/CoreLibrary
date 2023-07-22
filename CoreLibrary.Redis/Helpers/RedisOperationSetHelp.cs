using StackExchange.Redis;

namespace CoreLibrary.Redis
{
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value"></param>
        public async Task<bool> SetAddAsync<T>(string value, bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
            //反射实体的信息
            var type = typeof(T);
            string key = GetRedisKey(type.Name, Enums.EKeyOperator.Set, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SetAddAsync(key, value);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public async Task<bool> SetRemoveAsync<T>(string value, bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
            //反射实体的信息
            var type = typeof(T);
            string key = GetRedisKey(type.Name, Enums.EKeyOperator.Set, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SetRemoveAsync(key, value);
        }
        /// <summary>
        /// 取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async Task<string[]> SetGetAsync<T>(bool isContainsRedisPrefix = true)
        {
            //反射实体的信息
            var type = typeof(T);
            string key = GetRedisKey(type.Name, Enums.EKeyOperator.Set, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.SetMembersAsync(key)).ToStringArray();
        }
        /// <summary>
        /// 取值
        /// </summary>
        public async Task<string[]> SetGetAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.SetMembersAsync(GetRedisKey(key, Enums.EKeyOperator.Set, isContainsRedisPrefix))).ToStringArray();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> SetAddAsync(string key, string value, bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SetAddAsync(GetRedisKey(key, Enums.EKeyOperator.Set, isContainsRedisPrefix), value);
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<bool> SetRemoveAsync(string key, string value, bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(nameof(value));
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SetRemoveAsync(GetRedisKey(key, Enums.EKeyOperator.Set, isContainsRedisPrefix), value);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task<long> SetAddAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true)
        {

            if (value == null || value.Count <= 0)
                throw new ApplicationException("值不能为空");
            List<RedisValue> redisValues = new List<RedisValue>();
            foreach (var item in value)
            {
                redisValues.Add(await item.ToJsonAsync());
            }
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SetAddAsync(GetRedisKey(key, Enums.EKeyOperator.Set, isContainsRedisPrefix), redisValues.ToArray());
        }
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public async Task<List<T>> SetGetAsync<T>(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            var vList = await _redisConnection.Database.SetMembersAsync(GetRedisKey(key, Enums.EKeyOperator.Set, isContainsRedisPrefix));
            List<T> result = new List<T>();
            foreach (var item in vList)
            {
                var model = await item.ToStr().JsonToAsync<T>(); //反序列化
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 获取Set集合元素个数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SetLengthAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SetLengthAsync(GetRedisKey(key, Enums.EKeyOperator.Set, isContainsRedisPrefix));
        }
    }
}
