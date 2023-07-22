namespace CoreLibrary.Redis
{
    /// <summary>
    /// redis的连接配置
    /// </summary>
    public partial interface IRedisOperation : IRedisDependency
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <param name="hashField">需要删除的字段</param>
        /// <returns></returns>
        Task<bool> HashDeleteAsync(string key, string hashField, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="hashFields">需要删除的字段</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> HashDeleteAsync(string key, string[] hashFields, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 验证是否存在指定列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<bool> HashExistsAsync(string key, string hashField, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<string> HashGetAsync(string key, string hashField, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<T> HashGetAsync<T>(string key, string hashField, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<Dictionary<string, string>> HashGetAllAsync(string key, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>  
        /// <returns></returns>
        Task<string[]?> HashGetAsync(string key, string[] hashFields, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<List<T>?> HashGetAsync<T>(string key, string[] hashFields, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取hash的长度
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<long> HashLengthAsync(string key, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 存储hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields">存储的数据key-value结构</param>
        /// <param name="isRetuen">是否立即返回</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task HashSetAsync(string key, HashEntry[] hashFields, bool isContainsRedisPrefix = true, bool isRetuen = false);

        /// <summary>
        /// 储存单条hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField">字段名</param>
        /// <param name="value">值</param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<bool> HashSetAsync(string key, string hashField, string value, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 返回所有值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<string[]> HashValuesAsync(string key, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 返回所有值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<List<T>> HashValuesAsync<T>(string key, bool isContainsRedisPrefix = true);


        /// <summary>
        /// 获取指定的列的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<RedisValue> HashGetWithRedisValueAsync(string key, string hashField,
            bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<HashEntry[]> HashGetAllWithEntryAsync(string key, bool isContainsRedisPrefix = true);

        /// <summary>
        /// 获取多条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <param name="isContainsRedisPrefix">拼接key的时候 是否包含指定的RedisPrefix 前缀</param>
        /// <returns></returns>
        Task<RedisValue[]> HashGetWithRedisValuesAsync(string key, string[] hashFields,
            bool isContainsRedisPrefix = true);
    }
}