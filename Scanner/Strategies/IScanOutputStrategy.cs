using ScannerEmulator.Device;

namespace ScannerEmulator.Strategies
{
    public interface IScanOutputStrategy
    {
        Task ScanAndSave(IDevice device, string path, string outputName);
    }
}
