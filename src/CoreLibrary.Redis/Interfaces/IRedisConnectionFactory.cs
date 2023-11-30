using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Redis.Interfaces
{
    /// <summary>
    /// 连接工厂
    /// </summary>
    public interface IRedisConnectionFactory : IDisposable, IRedisDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        IRedisConnection Get(string key = "default");

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
