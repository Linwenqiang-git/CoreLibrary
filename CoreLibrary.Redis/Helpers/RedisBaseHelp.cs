using StackExchange.Redis;
using CoreLibrary.Core;
using System.Net;

namespace CoreLibrary.Redis
{
    /// <summary>
    /// Redis基础类
    /// </summary>
    public static class RedisBaseHelp 
    {
        /// <summary>
        /// RedisKey集合转数组
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static RedisKey[] ConvertRedisKeys(List<string> val)
        {
            return val.Select(k => (RedisKey)k).ToArray();
        }
        /// <summary>
        /// RedisKey集合转数组
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static RedisValue[] ConvertRedisValues(string[] val)
        {
            return val.Select(k => (RedisValue)k).ToArray();
        }
        /// <summary>
        /// 生成EndPoint
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        private static EndPoint ParseEndPoints(string host, int port)
        {
            if (IPAddress.TryParse(host, out IPAddress? ip)) return new IPEndPoint(ip, port);
            return new DnsEndPoint(host, port);
        }
        /// <summary>
        /// 生成EndPoint
        /// </summary>
        /// <param name="hostAndPort"></param>
        /// <returns></returns>
        public static EndPoint ParseEndPoints(string hostAndPort)
        {
            if (hostAndPort.IndexOf(":", StringComparison.Ordinal) != -1)
            {
                var obj = hostAndPort.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                var host = obj[0];
                var port = obj[1].ToInt();
                return ParseEndPoints(host, port);
            }
            else
            {
                throw new ApplicationException("hostAndPort error");
            }
        }
    }
}
