using LottoSheli.SendPrinter.Settings.Settings;
using Microsoft.Extensions.Configuration;

namespace LottoSheli.SendPrinter.Settings.Factory.ScannerSettings
{
    public class ScannerSettingsService : Service<ScannerSettings>
    {
        public ScannerSettingsService(IConfiguration config) : base(config)
        {
            
        }

        protected override string SectionName => "ScannerSettings";

        public ScannerSettings Get()
        {
            return this.CurrentSettings;
        }

        public void Save(ScannerSettings settings)
        {
            SaveData(settings);
        }
    }
}
