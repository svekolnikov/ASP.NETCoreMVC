using ScannerEmulator.Device;
using ScannerEmulator.Strategies;

namespace ScannerEmulator.Context
{
    public class DefaultScannerContext : ScannerContext
    {
        public DefaultScannerContext(IDevice device, IScanOutputStrategy outputStrategy)
        : base(device, outputStrategy)
        {}

        public async Task ScanExecute(string path, string outputName)
        {
           await OutputStrategy.ScanAndSave(Device, path, outputName);
        }
    }
}
