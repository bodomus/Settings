using LottoSheli.SendPrinter.Settings.Settings;
using Microsoft.Extensions.Configuration;

namespace LottoSheli.SendPrinter.Settings.Factory.OcrSettings
{
    public class OcrSettingsService : Service<OcrSettings>
    {
        protected override string SectionName => "OcrSettings";

        public OcrSettingsService(IConfiguration config) : base(config)
        {
        }
    }
}
