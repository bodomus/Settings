using Microsoft.Extensions.DependencyInjection;
using System;
using UAM.VerifyEmployee.Factory;

namespace LottoSheli.SendPrinter.Bootstraper
{
    class DependencyInjectionSettingsFactory : ISettingsFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionSettingsFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public IScannerSettings GetSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IScannerSettings>();
        }

        //public IScannerSettings GetScannerSettings()
        //{
        //    return _serviceProviderStrategy().GetRequiredService<IScannerSettings>();
        //}

        //public IRemoteSettings GetRemoteSettings()
        //{
        //    return _serviceProviderStrategy().GetRequiredService<IRemoteSettings>();
        //}
    }
}
