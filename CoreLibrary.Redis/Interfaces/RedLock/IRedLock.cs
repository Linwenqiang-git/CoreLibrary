using System;

namespace CoreLibrary.Redis.Interfaces.RedLock
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRedLock : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// 锁定的资源id
        /// </summary>
        string Resource { get; }

        /// <summary>
        /// 锁定的值信息
        /// </summary>
        string LockId { get; }

        /// <summary>
        /// 是否获取到锁 true 代表 获取到锁
        /// </summary>
        bool IsAcquired { get; }

        /// <summary>
        /// 锁的状态
        /// </summary>
        RedLockStatus Status { get; }

        /// <summary>
        /// Details of the number of instances the lock was able to be acquired in.
        /// </summary>
        RedLockInstanceSummary InstanceSummary { get; }

        /// <summary>
        /// 锁的有效时间延长次数
        /// </summary>
        int ExtendCount { get; }
    }
}