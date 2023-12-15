using LottoSheli.SendPrinter.Settings.Factory.OcrSettings;
using LottoSheli.SendPrinter.Settings.Factory.RemoteSettings;
using LottoSheli.SendPrinter.Settings.Factory.ScannerSettings;

namespace LottoSheli.SendPrinter.Settings.Factory
{
    public interface ISettingsFactory
    {
        IScannerSettings GetScannerSettings();

        void SaveScannerSettings(ScannerSettings.ScannerSettings settings);

        IRemoteSettings GetRemoteSettings();

        IOcrSettings GetOcrSettings();
    }
}
