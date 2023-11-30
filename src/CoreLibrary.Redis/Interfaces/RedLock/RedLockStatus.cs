namespace CoreLibrary.Redis.Interfaces.RedLock
{
	public enum RedLockStatus
	{
		/// <summary>
		/// 锁已释放
		/// </summary>
		Unlocked,

		/// <summary>
		/// 成功获取到锁
		/// </summary>
		Acquired,

		/// <summary>
		/// The lock was not acquired because there was no quorum available.
		/// </summary>
		NoQuorum,

		/// <summary>
		/// The lock was not acquired because it is currently locked with a different LockId.
		/// </summary>
		Conflicted,

		/// <summary>
		/// The lock expiry time passed before lock acquisition could be completed.
		/// </summary>
		Expired
	}
}