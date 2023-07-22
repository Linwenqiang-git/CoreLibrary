using Microsoft.Extensions.Options;
using UFX.Infra.Extensions;
using CoreLibrary.Redis.Interfaces;
using CoreLibrary.Redis.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLibrary.Redis.Extensions;
using System.Buffers;
using CoreLibrary.Redis.Const;

namespace CoreLibrary.Redis.Helpers
{
    /// <summary>
    /// Redis操作类
    /// </summary>
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="hashField">需要删除的字段</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string key, string hashField, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashDeleteAsync(key, hashField);
        }

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="hashFields">需要删除的字段</param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string key, string[] hashFields, bool isContainsRedisPrefix = true)
        {
            if (hashFields == null || !hashFields.Any())
                return default;
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashDeleteAsync(key, hashFields.ToRedisValueArray());
        }

        /// <summary>
        /// 验证是否存在指定列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string key, string hashField, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashExistsAsync(key, hashField);
        }

        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<string> HashGetAsync(string key, string hashField, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            var res = await _redisConnection.Database.HashGetAsync(key, hashField);
            return !res.IsNull ? res.ToStr() : default;
        }

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> HashGetAllAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            var res = await _redisConnection.Database.HashGetAllAsync(key);
            return res.ToStringDictionary();
        }

        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<string[]> HashGetAsync(string key, string[] hashFields, bool isContainsRedisPrefix = true)
        {
            if (hashFields == null || !hashFields.Any())
                return default;
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            var res = await _redisConnection.Database.HashGetAsync(key, hashFields.ToRedisValueArray());
            if (res is {Length: <= 0})
            {
                return default;
            }

            return res.Where(a => !a.IsNull).ToArray()?.ToStringArray();
        }

        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<List<T>> HashGetAsync<T>(string key, string[] hashFields, bool isContainsRedisPrefix = true)
        {
            if (hashFields == null || !hashFields.Any())
                return default;
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            var res = await _redisConnection.Database.HashGetAsync(key, hashFields.ToRedisValueArray());
            List<T> result = new List<T>();
            if (res != null && res.Length > 0)
            {
                foreach (var item in res)
                    if (!item.IsNull)
                        result.Add(await item.ToString().JsonToAsync<T>());
            }

            return result;
        }

        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string key, string hashField, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            var res = await _redisConnection.Database.HashGetAsync(key, hashField);
            return !res.IsNull ? await res.ToStr().JsonToAsync<T>() : default;
        }

        /// <summary>
        /// 获取hash的长度
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<long> HashLengthAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashLengthAsync(key);
        }

        /// <summary>
        /// 存储hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields">存储的数据key-value结构</param>
        /// <param name="isReturn">是否立即返回</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task HashSetAsync(string key, HashEntry[] hashFields, bool isContainsRedisPrefix = true,
            bool isReturn = false)
        {
            if (hashFields == null || hashFields.Count() <= 0)
                return;
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            await _redisConnection.Database.HashSetAsync(key, hashFields,
                isReturn ? CommandFlags.FireAndForget : CommandFlags.None);
        }

        /// <summary>
        /// 储存单条hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField">字段名</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync(string key, string hashField, string value,
            bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashSetAsync(key, hashField, value, When.Always,
                CommandFlags.FireAndForget);
        }

        /// <summary>
        /// 返回所有值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<string[]> HashValuesAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            var res = await _redisConnection.Database.HashValuesAsync(key);
            return res != null ? res.ToStringArray() : default;
        }

        /// <summary>
        /// 返回所有值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<List<T>> HashValuesAsync<T>(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            //获取数据
            var data = await _redisConnection.Database.HashValuesAsync(key);
            if (data == null || data.Length <= 0)
            {
                return new List<T>(0);
            }

            //拼接json
            var stringBuilder = new StringBuilder();
            var result = new List<T>();
            stringBuilder.Append("[");
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i]);
                if (i < data.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            stringBuilder.Append("]");
            return await stringBuilder.ToString().JsonToAsync<List<T>>();
        }

        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<RedisValue> HashGetWithRedisValueAsync(string key, string hashField,
            bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashGetAsync(key, hashField);
        }

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<HashEntry[]> HashGetAllWithEntryAsync(string key, bool isContainsRedisPrefix = true)
        {
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashGetAllAsync(key);
        }

        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        public async Task<RedisValue[]> HashGetWithRedisValuesAsync(string key, string[] hashFields,
            bool isContainsRedisPrefix = true)
        {
            if (hashFields == null || !hashFields.Any())
                return default;
            await _redisConnection.CreateConnectionAsync();
            key = GetRedisKey(key, Enums.EKeyOperator.Hash, isContainsRedisPrefix);
            return await _redisConnection.Database.HashGetAsync(key, hashFields.ToRedisValueArray());
        }
    }
}