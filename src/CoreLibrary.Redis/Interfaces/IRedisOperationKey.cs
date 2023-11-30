using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreLibrary.Redis.Enums;

namespace CoreLibrary.Redis.Interfaces
{
    /// <summary>
    /// redis的连接配置
    /// </summary>
    public partial interface IRedisOperation : IRedisDependency
    {
        /// <summary>
        /// 获取redis的key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        /// <param name="isContainsRedisPrefix"></param>
        /// <returns></returns>
        string GetRedisKey(string key, EKeyOperator eKeyOperator = default, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        /// <param name="eKeyOperator"></param>
        Task<bool> KeyRemoveAsync(string key, EKeyOperator eKeyOperator = default, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 移除key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="eKeyOperator"></param>
        Task<long> KeyRemoveAsync(List<string> key, EKeyOperator eKeyOperator = default, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="eKeyOperator"></param>
        Task<bool> KeyExistsAsync(string key, EKeyOperator eKeyOperator = default, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 设置Key过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiry"></param>
        /// <param name="eKeyOperator"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string key, TimeSpan? expiry = default, EKeyOperator eKeyOperator = default, bool isContainsRedisPrefix = true);
    }
}
