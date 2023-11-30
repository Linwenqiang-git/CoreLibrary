using StackExchange.Redis;

namespace CoreLibrary.Redis
{
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 存储list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async Task ListSetAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true)
        {
            if (value != null && value.Count > 0)
            {
                await _redisConnection.CreateConnectionAsync();
                foreach (var single in value)
                {
                    var val = await single.ToJsonAsync();
                    await _redisConnection.Database.ListRightPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), val);
                }
            }
        }
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public async Task<List<T>> ListGetAsync<T>(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            var vList = await _redisConnection.Database.ListRangeAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix));
            List<T> result = new List<T>();
            foreach (var item in vList)
            {
                result.Add(await item.ToStr().JsonToAsync<T>());//反序列化
            }
            return result;
        }
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <param name="key"></param>
        public async Task<List<string>> ListGetAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            var vList = await _redisConnection.Database.ListRangeAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix));
            return vList.ToStringArray().ToList();
        }
        /// <summary>
        /// 删除list集合的某一项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">value值</param>
        /// <param name="count"></param>
        public async Task<long> ListRemoveAsync<T>(string key, T value, long count = 0, bool isContainsRedisPrefix = true)
        {
            if (value == null)
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.ListRemoveAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), value.ToJson(), count);
        }


        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.ListLengthAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix));
        }

        /// <summary>
        /// 删除并返回存储在key上的列表的第一个元素。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.ListLeftPopAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix));
        }
        /// <summary>
        /// 往最前推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task ListLeftPushAsync(string key, string value, bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            await _redisConnection.Database.ListLeftPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), value);
        }
        /// <summary>
        /// 往最后推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task ListRightPushAsync(string key, string value, bool isContainsRedisPrefix = true)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            await _redisConnection.Database.ListRightPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), value);
        }
        /// <summary>
        /// 删除并返回存储在key上的列表的第一个元素。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            var str = await _redisConnection.Database.ListLeftPopAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix));
            if (!str.HasValue)
            {
                return default;
            }
            return await str.ToStr().JsonToAsync<T>();
        }
        /// <summary>
        /// 往最前推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string key, T value, bool isContainsRedisPrefix = true)
        {
            if (value == null)
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            var val = await value.ToJsonAsync();
            return await _redisConnection.Database.ListLeftPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), val);
        }
        /// <summary>
        /// 往最后推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, T value, bool isContainsRedisPrefix = true)
        {
            if (value == null)
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            var val = await value.ToJsonAsync();
            return await _redisConnection.Database.ListRightPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), val);
        }
        /// <summary>
        /// 往最前推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true)
        {
            if (value == null || value.Count <= 0)
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            List<RedisValue> redisValues = new List<RedisValue>();
            foreach (var item in value)
            {
                redisValues.Add(await item.ToJsonAsync());
            }
            return await _redisConnection.Database.ListLeftPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), redisValues.ToArray());
        }
        /// <summary>
        /// 往末尾推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true)
        {
            if (value == null || value.Count <= 0)
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            List<RedisValue> redisValues = new List<RedisValue>();
            foreach (var item in value)
            {
                redisValues.Add(await item.ToJsonAsync());
            }
            return await _redisConnection.Database.ListRightPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), redisValues.ToArray());
        }
        /// <summary>
        /// 往最前推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string key, string[] value, bool isContainsRedisPrefix = true)
        {
            if (value == null || value.Length <= 0)
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.ListLeftPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), value.ToRedisValueArray());
        }
        /// <summary>
        /// 往末尾推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string key, string[] value, bool isContainsRedisPrefix = true)
        {
            if (value == null || value.Length <= 0)
                throw new ArgumentException("值不能为空");
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.ListRightPushAsync(GetRedisKey(key, Enums.EKeyOperator.List, isContainsRedisPrefix), value.ToRedisValueArray());
        }
    }
}
