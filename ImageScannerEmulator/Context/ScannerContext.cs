using ImageScannerEmulator.Device;
using ImageScannerEmulator.Strategies;

namespace ImageScannerEmulator.Context
{
    public abstract class ScannerContext
    {
        protected IDevice Device;
        protected IScanOutputStrategy OutputStrategy;
        protected FileSystemWatcher Watcher;

        private string? _savePath;

        protected ScannerContext(IDevice device, IScanOutputStrategy outputStrategy)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
            OutputStrategy = outputStrategy ?? throw new ArgumentNullException(nameof(outputStrategy));

            Watcher = new FileSystemWatcher();
            Watcher.Filter = "*.*";
            Watcher.Created += OnChanges;
        }

        public virtual void SetDevice(IDevice device) => Device = device;

        public virtual void SetStrategy(IScanOutputStrategy outputStrategy) => OutputStrategy = outputStrategy;

        private void OnChanges(object sender, FileSystemEventArgs e)
        {
            OutputStrategy.ScanAndSave(Device, e.FullPath, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss fff"));
        }

        public virtual void WatchRun(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));

            var currentPath = Directory.GetCurrentDirectory();

            if (!Directory.Exists(Path.Combine(currentPath,path)))
            {
                Directory.CreateDirectory(path);
            }

            Watcher.Path = path;
            Watcher.EnableRaisingEvents = true;
        }
        public virtual void WatchStop() => Watcher.EnableRaisingEvents = false;
    }
}
