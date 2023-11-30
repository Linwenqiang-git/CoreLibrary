using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Redis.Interfaces
{
    /// <summary>
    /// redis操作工厂 可以创建多个连接
    /// </summary>
    public interface IRedisOperationFactory : IRedisDependency,IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IRedisOperation Get(string key);
    }
}
