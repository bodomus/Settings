using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UAM.VerifyEmployee.Factory;

namespace UAM.VerifyEmployee.Settings
{
    public class RemoteSettings: SettingsBase<RemoteSettings>, IRemoteSettings
    {
        public string SectionName => "RemoteSettings";

        public RemoteSettings(IConfiguration root)
        {
            ConfigurationManager conf = new ConfigurationManager();
            var value = conf.GetSection("CommonSettings").Value;

            this.Load(SectionName);
        }
    }
}
