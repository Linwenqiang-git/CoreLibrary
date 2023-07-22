using StackExchange.Redis;

namespace CoreLibrary.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RedisOperationHelp
    {
        /// <summary>
        /// 消息确定
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupName"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> StreamAckAsync(string key, string groupName, string messageId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(groupName);
            ArgumentNullException.ThrowIfNull(messageId);
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.StreamAcknowledgeAsync(key, groupName, messageId)) > 0;
        }
        /// <summary>
        /// 确认消息并且删除队列的消息
        /// </summary>
        /// <param name="key">存储的key</param>
        /// <param name="groupName">消费者组名</param>
        /// <param name="messageId">消息id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StreamAckDeleteAsync(string key, string groupName, string messageId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(messageId);
            await _redisConnection.CreateConnectionAsync();
            await _redisConnection.Database.ScriptEvaluateAsync(AckDeleteScript, new RedisKey[] {
                 key
            }, new RedisValue[] {
                messageId,
                groupName
            });
        }

        /// <summary>
        /// 获取流中的消费者组消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<StreamGroupInfo[]> StreamGroupInfoAsync(string key, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.StreamGroupInfoAsync(key));
        }


        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <param name="messageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> StreamAddAsync(string key, IDictionary<string, byte[]> values, string messageId = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            if (values == null || values.Count <= 0)
                throw new ArgumentNullException(nameof(values));
            RedisValue? _messageId = messageId == null ? null : new RedisValue?(messageId);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StreamAddAsync(new StackExchange.Redis.RedisKey(key), values.Select(a => new NameValueEntry(a.Key, a.Value)).ToArray(), _messageId);
        }
        /// <summary>
        /// 流新增脚本
        /// </summary>
        private static readonly string BuckAddScript = EmbeddedResourceLoader.GetEmbeddedResource("CoreLibrary.Redis.Lua.StreamBuckAdd.lua");

        /// <summary>
        /// 流确认删除脚本
        /// </summary>
        private static readonly string AckDeleteScript = EmbeddedResourceLoader.GetEmbeddedResource("CoreLibrary.Redis.Lua.StreamAckDelete.lua");
        /// <summary>
        /// 批量新增消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string[]> StreamBuckAddAsync(string key, string field, List<byte[]> values, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            if (values == null || values.Count <= 0)
                throw new ArgumentNullException(nameof(values));
            var redisValuesList = new RedisValue[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                redisValuesList[i] = values[i];
            }
            await _redisConnection.CreateConnectionAsync();
            var res = await _redisConnection.Database.ScriptEvaluateAsync(BuckAddScript, new RedisKey[] {
                 key,
                 field
            }, redisValuesList);
            if (res.IsNull)
            {
                return default;
            }
            return ((string[])res);
        }

        /// <summary>
        /// 批量新增消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field">存储到value里面对应的键名</param>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string[]> StreamBuckAddAsync(string key, string field, List<string> values, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            if (values == null || values.Count <= 0)
                throw new ArgumentNullException(nameof(values));
            var redisValuesList = new RedisValue[values.Count];
            for (int i = 0; i < values.Count; i++)
            {
                redisValuesList[i] = values[i];
            }
            await _redisConnection.CreateConnectionAsync();
            var res = await _redisConnection.Database.ScriptEvaluateAsync(BuckAddScript, new RedisKey[] {
                 key,
                 field
            }, redisValuesList);
            if (res.IsNull)
            {
                return default;
            }
            return ((string[])res);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> CreateConsumerGroupAsync(string key, string groupName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(groupName);
            await _redisConnection.CreateConnectionAsync();
            try
            {
                //当数据不存在 匹配不到记录的时候 会抛出异常 所以这里 包裹一个try
                //验证当前消费者组是否已经存在
                var groupInfolist = await GetConsumerGroupInfoAsync(key);
                if (groupInfolist != null && groupInfolist.Any(a => a.Name.Equals(groupName)))
                {
                    return true;
                }
            }
            catch 
            {
            }
            return await _redisConnection.Database.StreamCreateConsumerGroupAsync(key, groupName, StreamPosition.Beginning);
        }
        /// <summary>
        /// 获取组信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<StreamGroupInfo[]> GetConsumerGroupInfoAsync(string key, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StreamGroupInfoAsync(key);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="messageIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> StreamDeleteAsync(string key, string[] messageIds, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            if (messageIds == null || messageIds.Length <= 0)
                throw new ArgumentNullException(nameof(messageIds));
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.StreamDeleteAsync(key, messageIds.Select(a => new RedisValue(a)).ToArray())) > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="key"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task<StreamPendingInfo> StreamPendingAsync(string key, string groupName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(groupName);
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.StreamPendingAsync(key, groupName));
        }

        /// <summary>
        /// 修剪流的长度
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="key"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public async Task<long> StreamTrimAsync(string key, int maxLength, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.StreamTrimAsync(key, maxLength, true));
        }

        /// <summary>
        /// 获取消费者 未确定队列的消息
        /// </summary>
        /// <param name="key">流名称</param>
        /// <param name="groupName">消费者组名称</param>
        /// <param name="count">查询的消息数</param>
        /// <param name="consumerName">消费者名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(string key, string groupName, int count, string consumerName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            return (await _redisConnection.Database.StreamPendingMessagesAsync(key, groupName, count, consumerName));
        }
        /// <summary>
        /// 移植待处理消息的所有权
        /// </summary>
        /// <param name="key">流名称</param>
        /// <param name="groupName">消费者组名称</param>
        /// <param name="targetConsumer">将消息所有权移植到此消费者</param>
        /// <param name="minIdleTimeInMs">消息停留最小的时间 毫秒</param>
        /// <param name="messageIds">需要处理的消息id</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        public async Task StreamClaimAsync(string key, string groupName, string targetConsumer, long minIdleTimeInMs, string[] messageIds, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            _ = (await _redisConnection.Database.StreamClaimIdsOnlyAsync(key, groupName, targetConsumer, minIdleTimeInMs, RedisBaseHelp.ConvertRedisValues(messageIds)));
        }

        /// <summary>
        /// 删除流对应消费者组的 消费者
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupName"></param>
        /// <param name="consumerName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> StreamDeleteConsumerAsync(string key, string groupName, string consumerName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(groupName);
            ArgumentNullException.ThrowIfNull(consumerName);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StreamDeleteConsumerAsync(key, groupName, consumerName);
        }
        /// <summary>
        /// 从消费者组中读取数据
        /// </summary>
        /// <param name="streamPositions"></param>
        /// <param name="groupName"></param>
        /// <param name="consumerName"></param>
        /// <param name="countPerStream"></param>
        /// <param name="noAck"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, string groupName, string consumerName, int? countPerStream = null, bool noAck = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (streamPositions == null || streamPositions.Length < 0)
                throw new ArgumentNullException(nameof(streamPositions));
            ArgumentNullException.ThrowIfNull(groupName);
            ArgumentNullException.ThrowIfNull(consumerName);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StreamReadGroupAsync(streamPositions, groupName, consumerName, countPerStream, noAck);
        }

        /// <summary>
        /// 根据id 范围查询数据
        /// </summary>
        /// <param name="key">流名称</param>
        /// <param name="minId">最小id</param>
        /// <param name="maxId">最大id</param>
        /// <param name="count">读取数量</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<StreamEntry[]> StreamRangeAsync(string key, string? minId = null, string? maxId = null, int? count = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StreamRangeAsync(key, minId, maxId, count);
        }


        /// <summary>
        /// 查询流数量
        /// </summary>
        /// <param name="key">流名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<long> StreamLengthAsync(string key, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ArgumentNullException.ThrowIfNull(key);
            await _redisConnection.CreateConnectionAsync();
            return await _redisConnection.Database.StreamLengthAsync(key);
        }
    }
}
