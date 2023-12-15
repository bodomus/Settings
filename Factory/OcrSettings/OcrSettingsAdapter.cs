using LottoSheli.SendPrinter.Settings.Settings;
using Microsoft.Extensions.Configuration;

namespace LottoSheli.SendPrinter.Settings.Factory.OcrSettings
{
    public class OcrSettingsAdapter : SettingsBase<OcrSettings>
    {
        protected override string SectionName => "OcrSettings";

        public OcrSettingsAdapter(IConfiguration config) : base(config)
        {
        }
    }
}
