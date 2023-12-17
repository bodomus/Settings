using LottoSheli.SendPrinter.Settings.Factory.ScannerSettings;
using Microsoft.Extensions.DependencyInjection;
using System;
using LottoSheli.SendPrinter.Settings.Factory.RemoteSettings;
using LottoSheli.SendPrinter.Settings.Factory.OcrSettings;

using LottoSheli.SendPrinter.Settings.Factory.ScannerSettings;


namespace LottoSheli.SendPrinter.Settings.Factory
{
    class DependencyInjectionSettingsFactory : ISettingsFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionSettingsFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public ScannerSettings.ScannerSettings GetScannerSettings()
        {
             var s = _serviceProviderStrategy().GetRequiredService<ScannerSettingsService>();
             //_serviceProviderStrategy().GetRequiredService<ScannerSettingsService>().Save();
             return s.Get();
        }

        public void SaveScannerSettings(ScannerSettings.ScannerSettings settings)
        {
            
            var adapter = _serviceProviderStrategy().GetRequiredService<ScannerSettingsService>();
            adapter.Save(settings);
        }

        public IOcrSettings GetOcrSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IOcrSettings>();
        }

        public IRemoteSettings GetRemoteSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IRemoteSettings>();
        }
    }
}
