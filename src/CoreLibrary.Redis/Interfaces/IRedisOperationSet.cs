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
        /// 新增
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value"></param>
        Task<bool> SetAddAsync<T>(string value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value"></param>
        Task<bool> SetRemoveAsync<T>(string value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <typeparam name="T"></typeparam>
        Task<string[]> SetGetAsync<T>(bool isContainsRedisPrefix = true);
        /// <summary>
        /// 取值
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// </summary>
        Task<string[]> SetGetAsync(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>  
        /// <param name="value"></param>
        Task<bool> SetAddAsync(string key, string value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value"></param>
        Task<bool> SetRemoveAsync(string key, string value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        Task<long> SetAddAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        Task<List<T>> SetGetAsync<T>(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取Set集合元素个数
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> SetLengthAsync(string key, bool isContainsRedisPrefix = true);
    }
}
