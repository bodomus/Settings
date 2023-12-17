using LottoSheli.SendPrinter.Settings.Factory.OcrSettings;
using LottoSheli.SendPrinter.Settings.Factory.RemoteSettings;

namespace LottoSheli.SendPrinter.Settings.Factory
{
    public interface ISettingsFactory
    {
        ScannerSettings.ScannerSettings GetScannerSettings();

        void SaveScannerSettings(ScannerSettings.ScannerSettings settings);
        

        IRemoteSettings GetRemoteSettings();

        IOcrSettings GetOcrSettings();
    }
}
