using CoreLibrary.Redis.Enums;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreLibrary.Redis.Interfaces.RedLock;

namespace CoreLibrary.Redis.Interfaces
{
    /// <summary>
    /// redis的连接配置
    /// </summary>
    public partial interface IRedisOperation : IRedisDependency
    {
        /// <summary>
        ///分布式锁 需要 多主节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiryTime">锁的过期时间</param>
        /// <param name="retryTime">每一个轮询的间隔时间</param>
        /// <param name="waitTime">整个锁等待的最大时间 超过此时间获取失败</param>
        /// <param name="retryDelayMs">每一次锁获取的重试次数 默认400ms</param>
        /// <param name="retryCount">每一次锁获取的重试次数</param>
        /// <param name="isContainsRedisPrefix">是否包含前缀</param>
        /// <returns></returns>
        Task<IRedLock> LockAsync(string key, TimeSpan expiryTime, TimeSpan waitTime, TimeSpan retryTime, int? retryCount=default, int? retryDelayMs = default, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 分布式锁需要集群环境 连续获取三次之后 直接获取失败
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiryTime">锁的过期时间</param>
        /// <param name="retryDelayMs">每一次锁获取的重试次数 默认400ms</param>
        /// <param name="retryCount">每一次锁获取的重试次数</param>
        /// <param name="isContainsRedisPrefix">是否包含前缀</param>
        /// <returns></returns>
        Task<IRedLock> LockAsync(string key, TimeSpan expiryTime, int? retryCount = default, int? retryDelayMs = default, bool isContainsRedisPrefix = true);
    }
}
