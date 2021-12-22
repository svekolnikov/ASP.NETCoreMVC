using ImageScannerEmulator.Device;

namespace ImageScannerEmulator.Strategies
{
    public interface IScanOutputStrategy
    {
        Task ScanAndSave(IDevice device, string sourcePath, string destName);
    }
}
