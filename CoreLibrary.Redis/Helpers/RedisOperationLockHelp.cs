namespace CoreLibrary.Redis
{
    public partial class RedisOperationHelp
    {
        #region 分布式锁 
        /// <summary>
        /// 锁
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiryTime">锁的过期时间</param>
        /// <param name="retryTime">每一个轮询的间隔时间</param>
        /// <param name="waitTime">整个锁等待的最大时间 超过此时间获取失败</param>
        /// <param name="retryDelayMs">每一次锁获取的重试次数 默认400ms</param>
        /// <param name="retryCount">每一次锁获取的重试次数</param>
        /// <param name="isContainsRedisPrefix">是否包含前缀</param>
        /// <returns></returns>
        public async Task<IRedLock> LockAsync(string key, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, int? retryCount = default, int? retryDelayMs = default, bool isContainsRedisPrefix = true)
        {
            //
            var distributedLockFactory = _serviceProvider.GetRequiredService<IDistributedLockFactory>();
            return await distributedLockFactory.CreateLockAsync(GetRedisKey(key, Enums.EKeyOperator.Lock, isContainsRedisPrefix), expiryTime, waitTime, retryTime, retryCount, retryDelayMs);
        }

        /// <summary>
        /// 锁 连续获取三次之后 直接获取失败
        /// </summary>
        /// <param name="key"></param>
        /// <param name="retryDelayMs">每一次锁获取的重试次数 默认400ms</param>
        /// <param name="retryCount">每一个获取的重试次数</param>
        /// <param name="expiry">锁的过期时间</param>
        /// <param name="isContainsRedisPrefix">是否包含前缀</param>
        /// <returns></returns>
        public async Task<IRedLock> LockAsync(string key, TimeSpan expiry, int? retryCount = default, int? retryDelayMs = default, bool isContainsRedisPrefix = true)
        {
            return await LockAsync(key, expiry, default, default, retryCount, retryDelayMs, isContainsRedisPrefix);
        }
        #endregion
        
    }
}
