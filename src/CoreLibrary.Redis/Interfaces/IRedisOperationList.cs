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
        /// 存储list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value"></param>
        Task ListSetAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        Task<List<T>> ListGetAsync<T>(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 取list 集合
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param> 
        /// <param name="key"></param>
        Task<List<string>> ListGetAsync(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 删除list集合的某一项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">value值</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="count"></param>
        Task<long> ListRemoveAsync<T>(string key, T value, long count = 0, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 获取集合中的数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 删除并返回存储在key上的列表的第一个元素。
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往最前推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task ListLeftPushAsync(string key, string value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往最后推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value">value值</param>
        /// <returns></returns>
        Task ListRightPushAsync(string key, string value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 删除并返回存储在key上的列表的第一个元素。
        /// </summary>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string key, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往最前推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key, T value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往最后推送一个数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key, T value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往最前推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往末尾推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> ListRightPushAsync<T>(string key, List<T> value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往最前推送多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param> 
        /// <returns></returns>
        Task<long> ListLeftPushAsync(string key, string[] value, bool isContainsRedisPrefix = true);
        /// <summary>
        /// 往末尾推送多条数据ll
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string key, string[] value, bool isContainsRedisPrefix = true);
    }
}
