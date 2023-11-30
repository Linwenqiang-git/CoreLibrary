using CoreLibrary.Redis.Enums;

namespace CoreLibrary.Redis
{
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 获取redis的key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        /// <param name="isContainsRedisPrefix"></param>
        /// <returns></returns>
        public string GetRedisKey(string key, EKeyOperator eKeyOperator = default, bool isContainsRedisPrefix = true)
        {
            //验证租户前缀是否已经启用
            if (_tenantPrefix.IsEnabled && _currentTenantAccessor.Current is {TenantId: { }} &&
                _currentTenantAccessor.Current.TenantId.Value != default)
            {
                key = $"{_currentTenantAccessor.Current.TenantId}:{key}";
            }

            return (isContainsRedisPrefix ? _redisConnection.RedisPrefix : "") + eKeyOperator switch
            {
                EKeyOperator.String => RedisPrefixKey.StringPrefixKey + key,
                EKeyOperator.List => RedisPrefixKey.ListPrefixKey + key,
                EKeyOperator.Set => RedisPrefixKey.SetPrefixKey + key,
                EKeyOperator.Hash => RedisPrefixKey.HashPrefixKey + key,
                EKeyOperator.SortedSet => RedisPrefixKey.SortedSetPrefixKey + key,
                _ => key,
            };
        }

        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="eKeyOperator"></param>
        public async Task<bool> KeyRemoveAsync(string key, EKeyOperator eKeyOperator = default,
            bool isContainsRedisPrefix = true)
        {
            key = GetRedisKey(key, eKeyOperator, isContainsRedisPrefix);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.KeyDeleteAsync(key);
        }

        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        public async Task<long> KeyRemoveAsync(List<string> key, EKeyOperator eKeyOperator = default,
            bool isContainsRedisPrefix = true)
        {
            if (key == null || key.Count() <= 0)
            {
                throw new ArgumentException(nameof(key));
            }

            List<string> removeList = new List<string>();
            key.ForEach(item => { removeList.Add(GetRedisKey(item, eKeyOperator, isContainsRedisPrefix)); });
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.KeyDeleteAsync(RedisBaseHelp.ConvertRedisKeys(removeList));
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        public async Task<bool> KeyExistsAsync(string key, EKeyOperator eKeyOperator = default,
            bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, eKeyOperator, isContainsRedisPrefix);
            return await _redisConnection.Database.KeyExistsAsync(key);
        }

        /// <summary>
        /// 设置Key过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="eKeyOperator"></param>
        /// <returns></returns>
        public async Task<bool> KeyExpireAsync(string key, TimeSpan? expiry = default,
            EKeyOperator eKeyOperator = default, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, eKeyOperator, isContainsRedisPrefix);
            return await _redisConnection.Database.KeyExpireAsync(key, expiry);
        }
    }
}