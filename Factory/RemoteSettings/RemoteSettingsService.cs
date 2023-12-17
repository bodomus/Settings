using System;
using LottoSheli.SendPrinter.Settings.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LottoSheli.SendPrinter.Settings.Factory.RemoteSettings
{
    public class RemoteSettingsService : Service<RemoteSettings>
    {
        protected override string SectionName { get; } = "RemoteSettings";
        public string Resilent { get; }

        public RemoteSettingsService(IConfiguration configuration) : base(configuration)
        {


        }
    }
}
