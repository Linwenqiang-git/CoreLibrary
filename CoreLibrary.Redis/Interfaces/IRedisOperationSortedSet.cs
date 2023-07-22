using CoreLibrary.Redis.Enums;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Redis.Interfaces
{
    /// <summary>
    /// redis的连接配置
    /// </summary>
    public partial interface IRedisOperation : IRedisDependency
    {
        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="score"></param>
        /// <returns></returns>
        Task<bool> SortedSetAddAsync<T>(string key, T value, double score, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取SortedSet的数据
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> SortedSetGetAsync<T>(string key, double score, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取SortedSet的数据
        /// </summary>
        /// <param name="score">开始</param>
        /// <param name="take">条数</param>
        /// <returns></returns>
        Task<Dictionary<RedisValue, double>> SortedSetRangeByScoreWithScoresAsync(string key, double score,long take=-1L,
            bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> SortedSetLengthAsync(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 移除SortedSet
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// </summary>
        Task<bool> SortedSetRemoveAsync<T>(string key, T value, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 移除SortedSet
        /// 
        /// </summary>
        Task<long> SortedSetRemoveAsync(string key, double start, double stop, bool isContainsRedisPrefix = true);
    }
}
