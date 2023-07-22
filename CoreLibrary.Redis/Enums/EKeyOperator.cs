using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary.Redis.Enums
{
    /// <summary>
    /// 存储操作redis key的类型 
    /// </summary>
    public enum EKeyOperator
    {
        /// <summary>
        /// 字符串类型
        /// </summary>
        String,
        /// <summary>
        /// list类型
        /// </summary>
        List,
        /// <summary>
        /// set 类型
        /// </summary>
        Set,
        /// <summary>
        /// 有序集合 类型
        /// </summary>
        SortedSet,
        /// <summary>
        /// Hash 类型
        /// </summary>
        Hash,
        /// <summary>
        /// 锁
        /// </summary>
        Lock
    }
}
