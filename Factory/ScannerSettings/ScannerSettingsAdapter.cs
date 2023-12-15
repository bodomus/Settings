using LottoSheli.SendPrinter.Settings.Settings;
using Microsoft.Extensions.Configuration;

namespace LottoSheli.SendPrinter.Settings.Factory.ScannerSettings
{
    public class ScannerSettingsAdapter : SettingsBase<ScannerSettings>, IAdapter<ScannerSettings>, IScannerSettings
    {
        public ScannerSettingsAdapter(IConfiguration config) : base(config)
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

        public int Scanner_SnippetRectangle_X { get; set; }
        public int Scanner_SnippetRectangle_Y { get; set; }
        public int Scanner_SnippetRectangle_Width { get; set; }
        public int Scanner_SnippetRectangle_Height { get; set; }
        public int Scanner_SnippetSide { get; set; }
        public int Scanner_ImageAdjusments_Contrast { get; set; }
        public int Scanner_ImageAdjusments_Brightness { get; set; }
        public bool AutoupdateWinnersTemplate { get; set; }
        public bool CollectTrainingData { get; set; }
        public int MaxTemplateVariants { get; set; }
    }
}
