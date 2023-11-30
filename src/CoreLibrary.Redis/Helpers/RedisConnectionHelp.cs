using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using StackExchange.Redis;

namespace CoreLibrary.Redis
{
    /// <summary>    
    /// redis缓存链接
    /// </summary>
    public partial class RedisConnectionHelp : IRedisConnection
    {
        /// <summary>
        /// 获取redis的参数
        /// </summary>
        private readonly IOptions<RedisOption> _options;

        /// <summary>
        /// 
        /// </summary>
        private readonly object _lock = new();

        /// <summary>
        /// 信号
        /// </summary>
        private static SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// redis链接构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        private RedisConnectionHelp(IOptions<RedisOption> options, ILogger logger)
        {
            _logger = logger;
            _options = options;
        }
        /// <summary>
        /// 通用的Key前缀
        /// </summary>
        public string RedisPrefix => _options.Value != null ? _options.Value.RedisPrefix : string.Empty;

        /// <summary>
        /// redis密码
        /// </summary>
        public string RedisPassword => _options.Value != null ? _options.Value.Password : "";

        /// <summary>
        /// 默认访问存储库
        /// </summary>
        public int RedisDefaultDataBase => _options.Value?.DefaultDataBase ?? 0;

        /// <summary>
        /// redis连接字符串
        /// </summary>
        public string[] RedisConnectionConfig
        {
            get => _options.Value != null ? _options.Value.Connection : new string[] { };
        }

        /// <summary>
        /// 是否开启哨兵模式 true 开启
        /// </summary>
        private bool IsOpenSentinel => _options.Value is {IsOpenSentinel: true};

        /// <summary>
        /// 连接超时时间 毫秒
        /// </summary>
        public int DefaultConnectTimeout => _options.Value?.ConnectTimeout ?? 300;

        /// <summary>
        /// 异步超时时间 毫秒
        /// </summary>
        public int DefaultAsyncTimeout => _options.Value?.AsyncTimeout ?? 5000;

        private ISubscriber sentinelsub;

        /// <summary>
        /// 获取
        /// </summary>
        public ConnectionMultiplexer RedisConnection { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IDatabase Database => RedisConnection.GetDatabase(RedisDefaultDataBase);

        /// <summary>
        /// 释放
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
                if (RedisConnection != null)
                {
                    RedisConnection?.Close();
                    RedisConnection?.Dispose();
                    RedisConnection = null;
                }
                if (sentinelsub != null)
                {
                    sentinelsub?.Multiplexer?.Close();
                    sentinelsub?.Multiplexer?.Dispose();
                    sentinelsub = null;
                }
                GC.SuppressFinalize(this);
            }
        }
    }


    public partial class RedisConnectionHelp
    {
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static RedisConnectionHelp Create(IOptions<RedisOption> options, ILogger logger)
        {
            var connectionHelper = new RedisConnectionHelp(options, logger);
            //创建连接
            connectionHelper.CreateConnection();
            return connectionHelper;
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>

        private void CreateConnection()
        {
            if (RedisConnection is {IsConnected: true})
            {
                return;
            }
            lock (_lock)
            {
                if (RedisConnection is {IsConnected: true})
                {
                    return;
                }
                ConfigurationOptions configurationOptions = new ConfigurationOptions()
                {
                    AllowAdmin = true,
                    Password = RedisPassword,
                    DefaultDatabase = RedisDefaultDataBase,
                    ConnectTimeout = DefaultConnectTimeout,
                    AbortOnConnectFail = false,
                    AsyncTimeout = DefaultAsyncTimeout
                };
                if (RedisConnectionConfig == null || !RedisConnectionConfig.Any())
                {
                    throw new ArgumentNullException("请填写有效的redis连接字符串,Connection");
                }
                //获取连接的字符串
                var connections = RedisConnectionConfig.ToList();
                connections.ForEach(item =>
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        configurationOptions.EndPoints.Add(item);
                    }
                });
                RedisConnection = ConnectionMultiplexer.Connect(configurationOptions);

                //注册如下事件
                RedisConnection.ConnectionFailed += MuxerConnectionFailed;
                RedisConnection.ConnectionRestored += MuxerConnectionRestored;
                RedisConnection.ErrorMessage += MuxerErrorMessage;
                RedisConnection.ConfigurationChanged += MuxerConfigurationChanged;
                RedisConnection.HashSlotMoved += MuxerHashSlotMoved;
                RedisConnection.InternalError += MuxerInternalError;
                //是否订阅哨兵通知消息
                if (IsOpenSentinel)
                {
                    SubscribeSentinel();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task CreateConnectionAsync()
        {
            if (RedisConnection != null && RedisConnection.IsConnected)
            {
                return;
            }
            try
            {
                await _semaphoreSlim.WaitAsync();
                if (RedisConnection != null && RedisConnection.IsConnected)
                {
                    return;
                }
                ConfigurationOptions configurationOptions = new ConfigurationOptions()
                {
                    AllowAdmin = true,
                    Password = RedisPassword,
                    DefaultDatabase = RedisDefaultDataBase,
                    ConnectTimeout = DefaultConnectTimeout,
                    AbortOnConnectFail = false,
                    AsyncTimeout = DefaultAsyncTimeout
                };
                if (RedisConnectionConfig == null || !RedisConnectionConfig.Any())
                {
                    throw new ArgumentNullException("请填写有效的redis连接字符串,Connection");
                }
                //获取连接的字符串
                var connections = RedisConnectionConfig.ToList();
                connections.ForEach(item =>
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        configurationOptions.EndPoints.Add(item);
                    }
                });
                RedisConnection = await ConnectionMultiplexer.ConnectAsync(configurationOptions);

                //注册如下事件
                RedisConnection.ConnectionFailed += MuxerConnectionFailed;
                RedisConnection.ConnectionRestored += MuxerConnectionRestored;
                RedisConnection.ErrorMessage += MuxerErrorMessage;
                RedisConnection.ConfigurationChanged += MuxerConfigurationChanged;
                RedisConnection.HashSlotMoved += MuxerHashSlotMoved;
                RedisConnection.InternalError += MuxerInternalError;
                //是否订阅哨兵通知消息
                if (IsOpenSentinel)
                {
                    await SubscribeSentinelAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
        #region 哨兵集群
        /// <summary>
        /// 烧饼配置
        /// </summary>
        public static ConfigurationOptions sentineloption = new ConfigurationOptions()
        {
            TieBreaker = "",
            CommandMap = CommandMap.Sentinel,
            ServiceName = "mymaster"
        };
        /// <summary>
        /// 订阅哨兵主从切换
        /// </summary>
        /// <param name="sentineloptions"></param>
        /// <returns></returns>
        private void SubscribeSentinel(ConfigurationOptions sentineloptions = null)
        {
            //获取哨兵地址
            List<string> sentinelConfig = _options.Value.RedisSentinelIp.ToList() ?? new List<string>();
            //哨兵节点
            sentinelConfig.ForEach(a =>
            {
                var endPoint = RedisBaseHelp.ParseEndPoints(a);
                if (!sentineloption.EndPoints.Contains(endPoint))
                {
                    sentineloption.EndPoints.Add(a);
                }
            });
            sentineloptions ??= sentineloption;
            //我们可以成功的连接一个sentinel服务器，对这个连接的实际意义在于：当一个主从进行切换后，如果它外层有Twemproxy代理，我们可以在这个时机（+switch-master事件)通知你的Twemproxy代理服务器，并更新它的配置文件里的master服务器的地址，然后从起你的Twemproxy服务，这样你的主从切换才算真正完成。
            //一般没有代理服务器，直接更改从数据库配置文件，将其升级为主数据库。
            var connect = ConnectionMultiplexer.Connect(sentineloptions);
            sentinelsub = connect.GetSubscriber();
            sentinelsub.Subscribe("+switch-master", (ch, message) =>
            {
                //当redis主从切换，可在此更改redis的 项目配置中的redis主从信息
                _logger.LogInformation("监听到redis主从切换,{message}", message);
            });
        }
        /// <summary>
        /// 订阅哨兵主从切换
        /// </summary>
        /// <param name="sentineloptions"></param>
        /// <returns></returns>
        private async Task SubscribeSentinelAsync(ConfigurationOptions sentineloptions = null)
        {
            //获取哨兵地址
            List<string> sentinelConfig = _options.Value.RedisSentinelIp.ToList() ?? new List<string>();
            //哨兵节点
            sentinelConfig.ForEach(a =>
            {
                var endPoint = RedisBaseHelp.ParseEndPoints(a);
                if (!sentineloption.EndPoints.Contains(endPoint))
                {
                    sentineloption.EndPoints.Add(a);
                }
            });
            sentineloptions ??= sentineloption;
            //我们可以成功的连接一个sentinel服务器，对这个连接的实际意义在于：当一个主从进行切换后，如果它外层有Twemproxy代理，我们可以在这个时机（+switch-master事件)通知你的Twemproxy代理服务器，并更新它的配置文件里的master服务器的地址，然后从起你的Twemproxy服务，这样你的主从切换才算真正完成。
            //一般没有代理服务器，直接更改从数据库配置文件，将其升级为主数据库。
            var connect = await ConnectionMultiplexer.ConnectAsync(sentineloptions);
            sentinelsub = connect.GetSubscriber();
            sentinelsub.Subscribe("+switch-master", (ch, message) =>
            {
                //当redis主从切换，可在此更改redis的 项目配置中的redis主从信息
                _logger.LogInformation("监听到redis主从切换,{message}", message);
            });
        }
        #endregion
        #region 事件

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            _logger.LogInformation("Configuration changed: {endpoint}", e.EndPoint);
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            _logger.LogError("ErrorMessage: {message}", e.Message);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _logger.LogError("ConnectionRestored: {EndPoint}", e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            _logger.LogInformation("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            _logger.LogInformation("HashSlotMoved:NewEndPoint" + e.NewEndPoint + ", OldEndPoint" + e.OldEndPoint);
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            _logger.LogInformation("InternalError:Message:[{Message}]", e.Exception.Message);
        }
        #endregion 事件
    }
}
