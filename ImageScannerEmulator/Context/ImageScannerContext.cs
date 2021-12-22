using ImageScannerEmulator.Device;
using ImageScannerEmulator.Strategies;

namespace ImageScannerEmulator.Context
{
    public class ImageScannerContext : ScannerContext
    {
        public ImageScannerContext(IDevice device, IScanOutputStrategy outputStrategy)
        : base(device, outputStrategy)
        {}

        public async Task ScanExecute(string path, string outputName)
        {
           await OutputStrategy.ScanAndSave(Device, path, outputName);
        }
        
    }
}
