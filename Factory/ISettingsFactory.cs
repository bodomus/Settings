using LottoSheli.SendPrinter.Settings.Factory.OcrSettings;
using LottoSheli.SendPrinter.Settings.Factory.RemoteSettings;
using LottoSheli.SendPrinter.Settings.Factory.ScannerSettings;

namespace LottoSheli.SendPrinter.Settings.Factory
{
    public interface ISettingsFactory
    {
        ScannerSettings.ScannerSettings GetScannerSettings();

        void SaveScannerSettings(ScannerSettings.ScannerSettings settings);
        //void SaveScannerSettings(IScannerSettings settings);

        IRemoteSettings GetRemoteSettings();

        IOcrSettings GetOcrSettings();
    }
}
