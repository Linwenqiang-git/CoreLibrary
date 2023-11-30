// using Microsoft.Extensions.Options;
//
//
// using UFX.Infra.Extensions;
// using CoreLibrary.Redis.Const;
// using CoreLibrary.Redis.Interfaces;
// using CoreLibrary.Redis.Options;
// using StackExchange.Redis;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace CoreLibrary.Redis.Helpers
// {
//     [Obsolete("高版本推荐使用Stream替代")]
//     public partial class RedisOperationHelp
//     {
//         /// <summary>
//         /// 订阅消息
//         /// </summary>
//         /// <param name="chanel">订阅的名称</param>
//         /// <param name="handler">需要处理的事件</param>
//         /// <param name="flags"></param>
//         public async Task SubscribeAsync(RedisChannel chanel, Action<RedisChannel, RedisValue> handler, CommandFlags flags = CommandFlags.None)
//         {
//             await _redisConnection.CreateConnectionAsync();
//             var subscriber = _redisConnection.RedisConnection.GetSubscriber();
//             await subscriber.SubscribeAsync(chanel, handler, flags);
//         }
//         /// <summary>
//         /// 发布消息
//         /// </summary>
//         /// <param name="channel">被订阅的name</param>
//         /// <param name="message">需要传递的参数</param>
//         /// <param name="flags"></param>
//         public async Task<long> PublishAsync(RedisChannel channel, RedisValue message, CommandFlags flags = CommandFlags.None)
//         {
//             await _redisConnection.CreateConnectionAsync();
//             var subscriber = _redisConnection.RedisConnection.GetSubscriber();
//             return await subscriber.PublishAsync(channel, message, flags);
//         }
//         /// <summary>
//         /// 取消订阅
//         /// </summary>
//         /// <param name="chanel">订阅的名称</param>
//         /// <param name="handler">需要处理的事件</param>
//         /// <param name="flags"></param>
//         public async Task UnsubscribeAsync(RedisChannel chanel, Action<RedisChannel, RedisValue> handler = null, CommandFlags flags = CommandFlags.None)
//         {
//             await _redisConnection.CreateConnectionAsync();
//             var subscriber = _redisConnection.RedisConnection.GetSubscriber();
//             await subscriber.UnsubscribeAsync(chanel, handler, flags);
//         }
//         /// <summary>
//         /// 取消所有的订阅
//         /// </summary>
//         /// <param name="flags"></param>
//         public async Task UnsubscribeAllAsync(CommandFlags flags = CommandFlags.None)
//         {
//             await _redisConnection.CreateConnectionAsync();
//             var subscriber = _redisConnection.RedisConnection.GetSubscriber();
//             await subscriber.UnsubscribeAllAsync(flags);
//         }
//     }
// }
