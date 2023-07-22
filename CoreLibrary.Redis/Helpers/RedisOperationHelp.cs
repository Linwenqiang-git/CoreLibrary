namespace CoreLibrary.Redis
{
    public partial class RedisOperationHelp : IRedisOperation
    {
        /// <summary>
        /// 
        /// </summary>
        private IRedisConnection _redisConnection;

        /// <summary>
        /// 
        /// </summary>
        private readonly IRedisConnectionFactory _redisConnectionFactory;

        /// <summary>
        /// 
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        private readonly ICurrentTenantAccessor _currentTenantAccessor;
        private readonly IDataFilter<IHaveCacheTenantPrefix> _tenantPrefix;

        /// <summary>
        /// 
        /// </summary>
        private string _key;

        /// <summary>
        /// 实例化连接
        /// di初始化
        /// </summary>
        public RedisOperationHelp(IRedisConnectionFactory redisConnectionFactory, IServiceProvider serviceProvider,
            ICurrentTenantAccessor currentTenantAccessor)
        {
            _key = "default";
            _redisConnection = redisConnectionFactory.Get(_key);
            _serviceProvider = serviceProvider;
            _redisConnectionFactory = redisConnectionFactory;
            _tenantPrefix = _serviceProvider.GetRequiredService<IDataFilter<IHaveCacheTenantPrefix>>();
            _currentTenantAccessor = currentTenantAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="redisConnectionFactory"></param>
        /// <param name="serviceProvider"></param>
        private RedisOperationHelp(string key, IRedisConnectionFactory redisConnectionFactory,
            IServiceProvider serviceProvider, ICurrentTenantAccessor currentTenantAccessor)
        {
            _redisConnection = redisConnectionFactory.Get(key);
            _key = key;
            _serviceProvider = serviceProvider;
            _currentTenantAccessor = currentTenantAccessor;
            _redisConnectionFactory = redisConnectionFactory;
            _tenantPrefix = _serviceProvider.GetRequiredService<IDataFilter<IHaveCacheTenantPrefix>>();
        }

        /// <summary>
        /// 构建一个新redis对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="redisConnectionFactory"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        internal static IRedisOperation Build(string key, IRedisConnectionFactory redisConnectionFactory,
            IServiceProvider serviceProvider, ICurrentTenantAccessor currentTenantAccessor)
        {
            return new RedisOperationHelp(key, redisConnectionFactory, serviceProvider, currentTenantAccessor);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                _redisConnectionFactory.Remove(_key);
                _key = null;
                GC.SuppressFinalize(this);
            }
        }
    }
}