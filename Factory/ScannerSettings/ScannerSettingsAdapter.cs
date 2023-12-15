using LottoSheli.SendPrinter.Settings.Settings;
using Microsoft.Extensions.Configuration;

namespace LottoSheli.SendPrinter.Settings.Factory.ScannerSettings
{
    public class ScannerSettingsAdapter : SettingsBase<ScannerSettings>, IAdapter<ScannerSettings>
    {
        public ScannerSettingsAdapter(IConfiguration config) : base(config)
        {
            
        }





        protected override string SectionName => "ScannerSettings";

        public ScannerSettings Get()
        {
            return this.CurrentSettings;
        }

        public void Save<T>()
        {
            throw new System.NotImplementedException();
        }

        public void Save(ScannerSettings settings)
        {
            SaveData(settings);
            throw new System.NotImplementedException();
        }
    }
}
