using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using NLog;
using NLog.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using Microsoft.Extensions.Options;
using UAM.VerifyEmployee.Factory;
using UAM.VerifyEmployee.Settings;


namespace LottoSheli.SendPrinter.Bootstraper
{

    /// <summary>
    /// Provides Bootstraper objects factory
    /// </summary>
    public sealed class AbstarctObjectsFactory : IAbstractObjectsFactory
    {
        private readonly ServiceProvider _serviceProvider;
        private ILogger<AbstarctObjectsFactory> _logger;

        public AbstarctObjectsFactory(IConfiguration config, bool initOcr = true)
        {
            //Directory.CreateDirectory(Settings.LottoHome);

            var services = new ServiceCollection();
            services.Configure<CommonSettings>(config.GetSection("MyConfig"));
            services.AddSingleton<IAbstractObjectsFactory>(this);
            

            InitLogger(services, config);
            InitSettings(services);
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<IScannerSettings>(new ScannerSettings());
            _serviceProvider = services.BuildServiceProvider();
            var s = _serviceProvider.GetService<IOptions<CommonSettings>>();
            //_logger = GetLoggerFactory().CreateLogger<AbstarctObjectsFactory>();
            var remote = _serviceProvider.GetRequiredService<IRemoteSettings>();
        }

        private void InitSettings(ServiceCollection services)
        {
            services.AddSingleton<IRemoteSettings, RemoteSettings>();

        }


        private IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

        private static void InitLogger(IServiceCollection services, IConfiguration config)
        {
            services.AddLogging(loggingBuilder => {
                // configure Logging with NLog
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog(config);
            });

            LogManager.Setup().SetupExtensions(ext => ext.RegisterConfigSettings(config));
        }

        #region IObjectsFactory
        public TService GetService<TService>() => _serviceProvider.GetRequiredService<TService>();

        public ILoggerFactory GetLoggerFactory()
        {
            return _serviceProvider.GetService<ILoggerFactory>();
        }

        public ISettingsFactory GetSettingsFactory()
        {
            return _serviceProvider.GetRequiredService<ISettingsFactory>();
        }

        public IScannerSettings GetSettings()
        {
            return _serviceProvider.GetRequiredService<IScannerSettings>();
        }
        #endregion IObjectsFactory

        #region IDisposable
        private void Dispose(bool disposing)
        {

            if (disposing)
            {
                //(_serviceProvider.GetRequiredService<IDBCreatorFactory>() as IDisposable)?.Dispose();
                (_serviceProvider as IDisposable)?.Dispose();

            }

            //_serviceProvider = null;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable
    }
}
