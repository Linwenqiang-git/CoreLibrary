using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLibrary.Redis.Interfaces
{
    public partial interface IRedisOperation : IRedisDependency
    {
        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        Task<RedisResult> ScriptExecAsync(string script, string[] redisKey, string[] redisValue, CancellationToken cancellationToken = default);
        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        Task<RedisResult> ScriptExecAsync(string script, string[] redisKey, List<byte[]> redisValue, CancellationToken cancellationToken = default);
    }
}
