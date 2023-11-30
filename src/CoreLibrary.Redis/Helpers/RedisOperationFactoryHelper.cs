namespace CoreLibrary.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisOperationFactoryHelper : IRedisOperationFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private IRedisConnectionFactory _redisConnectionFactory;

        /// <summary>
        /// 
        /// </summary>
        private IServiceProvider _serviceProvider;        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisConnectionFactory"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="currentTenantAccessor"></param>
        public RedisOperationFactoryHelper(IRedisConnectionFactory redisConnectionFactory,
            IServiceProvider serviceProvider, ICurrentTenantAccessor currentTenantAccessor)
        {
            _redisConnectionFactory = redisConnectionFactory;
            _serviceProvider = serviceProvider;
            _currentTenantAccessor = currentTenantAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDispose"></param>
        protected virtual void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                _redisConnectionFactory = null;
                _serviceProvider = null;
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IRedisOperation Get(string key)
        {
            return RedisOperationHelp.Build(key, _redisConnectionFactory, _serviceProvider, _currentTenantAccessor);
        }
    }
}