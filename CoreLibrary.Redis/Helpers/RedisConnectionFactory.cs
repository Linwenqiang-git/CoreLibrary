using Microsoft.Extensions.DependencyInjection;

using System.Collections.Concurrent;

namespace CoreLibrary.Redis
{
    /// <summary>
    /// 
    /// </summary>
    internal class RedisConnectionFactory : IRedisConnectionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private ConcurrentDictionary<string, IRedisConnection> _redisConnection;

        /// <summary>
        /// 
        /// </summary>
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 
        /// </summary>
        public RedisConnectionFactory(IServiceProvider serviceProvider)
        {
            _redisConnection = new ConcurrentDictionary<string, IRedisConnection>();
            _serviceProvider = serviceProvider;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDispose"></param>
        protected virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                if (_redisConnection != null)
                {
                    foreach (var item in _redisConnection)
                    {
                        item.Value?.Dispose();
                    }
                    _redisConnection.Clear();
                    _redisConnection = null;
                }
                GC.SuppressFinalize(this);
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IRedisConnection Get(string key = "default")
        {
            if (_redisConnection.TryGetValue(key, out var redis))
            {
                return redis;
            }
            //因为是在构造函数中生成 所以不存在 并发
            var option = _serviceProvider.GetRequiredService<IOptions<RedisOption>>();
            var logger = _serviceProvider.GetRequiredService<ILogger<RedisConnectionHelp>>();
            redis = RedisConnectionHelp.Create(option, logger);
            _redisConnection.TryAdd(key, redis);
            return redis;
        }



        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (_redisConnection.TryRemove(key, out var redis))
            {
                redis?.Dispose();
                redis = null;
            }
        }
    }
}
