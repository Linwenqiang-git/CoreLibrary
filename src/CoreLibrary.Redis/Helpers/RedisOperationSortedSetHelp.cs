using StackExchange.Redis;

namespace CoreLibrary.Redis
{
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string key, T value, double score, bool isContainsRedisPrefix = true)
        {
            if (value == null)
            {
                throw new ArgumentException(nameof(value));
            }
            var val = await value.ToJsonAsync();
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SortedSetAddAsync(GetRedisKey(key, Enums.EKeyOperator.SortedSet, isContainsRedisPrefix), val, score);
        }
        /// <summary>
        /// 获取SortedSet的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> SortedSetGetAsync<T>(string key, double score, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            var result = await _redisConnection.Database.SortedSetRangeByScoreAsync(GetRedisKey(key, Enums.EKeyOperator.SortedSet, isContainsRedisPrefix), score);
            return await result.ToStr().JsonToAsync<T>();
        }
        /// <summary>
        /// 获取SortedSet的数据
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<RedisValue,double>> SortedSetRangeByScoreWithScoresAsync(string key, double score,long take=-1L, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            var result = await _redisConnection.Database.SortedSetRangeByScoreWithScoresAsync(GetRedisKey(key, Enums.EKeyOperator.SortedSet, isContainsRedisPrefix), score,double.PositiveInfinity, Exclude.None,Order.Ascending,0,take);
           return result.ToDictionary();
        }
        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SortedSetLengthAsync(GetRedisKey(key, Enums.EKeyOperator.SortedSet, isContainsRedisPrefix));
        }
        /// <summary>
        /// 移除SortedSet
        /// </summary>
        public async Task<bool> SortedSetRemoveAsync<T>(string key, T value, bool isContainsRedisPrefix = true)
        {
            if (value == null)
            {
                throw new ArgumentException(nameof(value));
            }
            var val = await value.ToJsonAsync();
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SortedSetRemoveAsync(GetRedisKey(key, Enums.EKeyOperator.SortedSet, isContainsRedisPrefix), val);
        }
        /// <summary>
        /// 移除SortedSet
        /// 
        /// </summary>
        public async Task<long> SortedSetRemoveAsync(string key, double start, double stop, bool isContainsRedisPrefix = true)
        {
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.SortedSetRemoveRangeByScoreAsync(GetRedisKey(key, Enums.EKeyOperator.SortedSet, isContainsRedisPrefix), start, stop);
        }
    }
}
