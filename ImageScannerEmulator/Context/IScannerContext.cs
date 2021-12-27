using ImageScannerEmulator.Device;
using ImageScannerEmulator.Strategies;

namespace ImageScannerEmulator.Context
{
    public interface IScannerContext
    {
        void SetDevice(IDevice device);
        void SetStrategy(IScanOutputStrategy outputStrategy);
        Task ScanExecute(string path, string destName);
        void WatchRun(string path);
        void WatchStop();
    }
}
