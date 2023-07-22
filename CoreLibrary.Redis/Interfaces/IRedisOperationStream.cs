using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLibrary.Redis.Interfaces
{
    /// <summary>
    /// redis
    /// </summary>
    public partial interface IRedisOperation : IRedisDependency
    {
        #region 异步

        /// <summary>
        /// 新增一条流数据
        /// </summary>
        /// <param name="key">存储的key</param>
        /// <param name="values">存储的值</param>
        /// <param name="messageId">消息id 不填写 默认生成</param>
        /// <returns></returns>
        Task<string> StreamAddAsync(string key, IDictionary<string, byte[]> values, string messageId = default, CancellationToken cancellationToken = default);
        /// <summary>
        /// 批量新增消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field">存储到value里面对应的键名</param>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string[]> StreamBuckAddAsync(string key, string field, List<byte[]> values, CancellationToken cancellationToken = default);
        /// <summary>
        /// 批量新增消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field">存储到value里面对应的键名</param>
        /// <param name="values"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string[]> StreamBuckAddAsync(string key, string field, List<string> values, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除流信息 根据消息id
        /// </summary>
        /// <param name="key">存储的key</param>
        /// <param name="messageIds">消息id信息</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> StreamDeleteAsync(string key, string[] messageIds, CancellationToken cancellationToken = default);
        /// <summary>
        /// 获取消费者中待处理队列信息
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="key"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task<StreamPendingInfo> StreamPendingAsync(string key, string groupName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 修剪流的长度
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="key"></param>
        /// <param name="maxLength">消息的最大长度</param>
        /// <returns></returns>
        Task<long> StreamTrimAsync(string key, int maxLength, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取消费者 未确定队列的消息
        /// </summary>
        /// <param name="key">流名称</param>
        /// <param name="groupName">消费者组名称</param>
        /// <param name="count">查询的消息数</param>
        /// <param name="consumerName">消费者名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<StreamPendingMessageInfo[]> StreamPendingMessagesAsync(string key, string groupName, int count, string consumerName, CancellationToken cancellationToken = default);


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
        Task StreamClaimAsync(string key, string groupName, string targetConsumer, long minIdleTimeInMs, string[] messageIds, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除流对应消费者组的 消费者
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupName"></param>
        /// <param name="consumerName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> StreamDeleteConsumerAsync(string key, string groupName, string consumerName, CancellationToken cancellationToken = default);
        /// <summary>
        /// 确认消息
        /// </summary>
        /// <param name="key">存储的key</param>
        /// <param name="groupName">消费者组名</param>
        /// <param name="messageId">消息id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> StreamAckAsync(string key, string groupName, string messageId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 确认消息并且删除队列的消息
        /// </summary>
        /// <param name="key">存储的key</param>
        /// <param name="groupName">消费者组名</param>
        /// <param name="messageId">消息id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task StreamAckDeleteAsync(string key, string groupName, string messageId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取流中的消费者组消息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<StreamGroupInfo[]> StreamGroupInfoAsync(string key, CancellationToken cancellationToken = default);
        /// <summary>
        /// 创建消费者组
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupName">组名</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> CreateConsumerGroupAsync(string key, string groupName, CancellationToken cancellationToken = default);
        /// <summary>
        /// 从指定的组读取流数据
        /// </summary>
        /// <param name="noAck"></param>
        /// <param name="streamPositions">流信息</param>
        /// <param name="groupName">消费者组名称</param>
        /// <param name="consumerName">消费者名称</param>
        /// <param name="cancellationToken">是否自动确认</param>
        /// <param name="countPerStream">每次读取的流的数据条数</param>
        /// <returns></returns>
        Task<RedisStream[]> StreamReadGroupAsync(StreamPosition[] streamPositions, string groupName, string consumerName, int? countPerStream = null, bool noAck = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据id 范围查询数据
        /// </summary>
        /// <param name="key">流名称</param>
        /// <param name="minId">最小id</param>
        /// <param name="maxId">最大id</param>
        /// <param name="count">读取数量</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<StreamEntry[]> StreamRangeAsync(string key, string? minId = null, string? maxId = null, int? count = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询流数量
        /// </summary>
        /// <param name="key">流名称</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> StreamLengthAsync(string key, CancellationToken cancellationToken = default);
        #endregion
    }
}
