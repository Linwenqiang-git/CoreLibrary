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
    public interface IRedisConnection : IDisposable
    {
        /// <summary>
        /// Key前缀
        /// </summary>
        string RedisPrefix { get; }
        /// <summary>
        /// redis 密码
        /// </summary>
        string RedisPassword { get; }
        /// <summary>
        /// redis的默认存储库
        /// </summary>

        int RedisDefaultDataBase { get; }
        /// <summary>
        /// redis 连接
        /// </summary>

        string[] RedisConnectionConfig { get; }
        /// <summary>
        /// 默认超时时间
        /// </summary>
        int DefaultConnectTimeout { get; }
        /// <summary>
        /// 默认异步超时时间
        /// </summary>
        int DefaultAsyncTimeout { get; }
        /// <summary>
        /// 连接实例
        /// </summary>
        ConnectionMultiplexer RedisConnection { get; }


        /// <summary>
        /// 
        /// </summary>
        IDatabase Database { get; }

        /// <summary>
        /// 
        /// </summary>
        Task CreateConnectionAsync();
    }
}
