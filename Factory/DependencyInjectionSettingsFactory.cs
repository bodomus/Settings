using LottoSheli.SendPrinter.Settings.Factory.ScannerSettings;
using Microsoft.Extensions.DependencyInjection;
using System;
using LottoSheli.SendPrinter.Settings.Factory.RemoteSettings;
using LottoSheli.SendPrinter.Settings.Factory.OcrSettings;


namespace LottoSheli.SendPrinter.Settings.Factory
{
    class DependencyInjectionSettingsFactory : ISettingsFactory
    {
        private Func<IServiceProvider> _serviceProviderStrategy;
        public DependencyInjectionSettingsFactory(Func<IServiceProvider> serviceProviderStrategy)
        {
            _serviceProviderStrategy = serviceProviderStrategy;
        }

        public IScannerSettings GetScannerSettings()
        {
             var s = _serviceProviderStrategy().GetRequiredService<ScannerSettingsAdapter>();
             //_serviceProviderStrategy().GetRequiredService<ScannerSettingsAdapter>().Save();
             return s.Get();
        }


        public void SaveScannerSettings(ScannerSettings.ScannerSettings settings)
        {
            _serviceProviderStrategy().GetRequiredService<ScannerSettingsAdapter>();
        }

        public IOcrSettings GetOcrSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IOcrSettings>();
        }

        public IRemoteSettings GetRemoteSettings()
        {
            return _serviceProviderStrategy().GetRequiredService<IRemoteSettings>();
        }

        public void SaveScannerSettings()
        {
            var adapter = _serviceProviderStrategy().GetRequiredService<ScannerSettingsAdapter>();
            var settings = adapter.Get();
            adapter.Save(settings);
            throw new NotImplementedException();
        }
    }
}
