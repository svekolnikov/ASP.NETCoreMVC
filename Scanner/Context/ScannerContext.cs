using ScannerEmulator.Device;
using ScannerEmulator.Strategies;

namespace ScannerEmulator.Context
{
    public abstract class ScannerContext
    {
        protected IDevice Device;
        protected IScanOutputStrategy OutputStrategy;

        protected ScannerContext(IDevice device, IScanOutputStrategy outputStrategy)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
            OutputStrategy = outputStrategy ?? throw new ArgumentNullException(nameof(outputStrategy));
        }

        public virtual void SetDevice(IDevice device) => Device = device;

        public virtual void SetStrategy(IScanOutputStrategy outputStrategy) => OutputStrategy = outputStrategy;

    }
}
