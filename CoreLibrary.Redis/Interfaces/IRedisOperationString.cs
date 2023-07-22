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
        /// 保存字符串
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// </summary>
        Task<bool> StringSetAsync(string key, string value, TimeSpan? expiry = default, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 保存字符串 当不存在的时候保存
        /// </summary>
        Task<bool> StringSetNotExistsAsync(string key, string value, TimeSpan? expiry = default, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <typeparam name="T"></typeparam>
        Task<bool> StringSetAsync<T>(string key, T value, TimeSpan? expiry = default, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 保存集合对象
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <typeparam name="T"></typeparam>
        Task<bool> StringSetAsync<T>(string key, List<T> value, TimeSpan? expiry = default, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取字符串
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// </summary>
        Task<string> StringGetAsync(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <typeparam name="T"></typeparam>
        Task<T> StringGetAsync<T>(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <typeparam name="T"></typeparam>
        Task<object> StringGetObjAsync(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="isContainsRedisPrefix"></param>
        /// <typeparam name="T"></typeparam>
        Task<List<T>> StringGetAsync<T>(List<string> keys, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 自增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> StringIncrementAsync(string key, long value = 1, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 自增 当达到limit将移除(返回0)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="limit">递增的最大数 当达到执行数将移除(返回0)</param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> StringIncrementLimitRemoveAsync(string key, long limit, long value = 1,
            bool isContainsRedisPrefix = true);
        /// <summary>
        /// 递减
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> StringDecrementAsync(string key, long value = 1, bool isContainsRedisPrefix = true);
    }
}
