using StackExchange.Redis;

namespace CoreLibrary.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<RedisResult> ScriptExecAsync(string script, string[] redisKey, string[] redisValue, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(script);
            ArgumentNullException.ThrowIfNull(redisKey);
            ArgumentNullException.ThrowIfNull(redisValue);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.ScriptEvaluateAsync(script, redisKey.Select(a => new RedisKey(a)).ToArray(), redisValue.Select(a => new RedisValue(a)).ToArray());
        }
        /// <summary>
        /// 执行脚本
        /// </summary>
        /// <param name="script"></param>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        public async Task<RedisResult> ScriptExecAsync(string script, string[] redisKey, List<byte[]> redisValue, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(script);
            ArgumentNullException.ThrowIfNull(redisKey);
            ArgumentNullException.ThrowIfNull(redisValue);
            var redisValues = new RedisValue[redisValue.Count];

            for (int i = 0; i < redisValue.Count; i++)
            {
                redisValues[i] = redisValue[i];
            }
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.ScriptEvaluateAsync(script, redisKey.Select(a => new RedisKey(a)).ToArray(), redisValues);
        }
    }
}
