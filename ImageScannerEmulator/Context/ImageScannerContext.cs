using ImageScannerEmulator.Device;
using ImageScannerEmulator.Logger;
using ImageScannerEmulator.Strategies;

namespace ImageScannerEmulator.Context
{
    public class ImageScannerContext : IScannerContext
    {
        private IDevice _device;
        private IScanOutputStrategy _outputStrategy;
        private readonly IScannerLogger _logger;
        private readonly FileSystemWatcher _watcher;

        public ImageScannerContext(IDevice device, IScanOutputStrategy outputStrategy, IScannerLogger logger)
        {
            _device = device ?? throw new ArgumentNullException(nameof(device));
            _outputStrategy = outputStrategy ?? throw new ArgumentNullException(nameof(outputStrategy));
            _logger = logger;

            _watcher = new FileSystemWatcher();
            _watcher.Filter = "*.*";
            _watcher.Created += OnChanges;
        }

        public void SetDevice(IDevice device) => _device = device;

        public void SetStrategy(IScanOutputStrategy outputStrategy) => _outputStrategy = outputStrategy;

        public void WatchRun(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(nameof(path));

            var currentPath = Directory.GetCurrentDirectory();

            if (!Directory.Exists(Path.Combine(currentPath, path)))
            {
                Directory.CreateDirectory(path);
            }

            _watcher.Path = path;
            _logger.WriteInfo("Start watching...");
            _watcher.EnableRaisingEvents = true;
        }

        public void WatchStop()
        {
            _logger.WriteInfo("Stopped watching");

            _watcher.EnableRaisingEvents = false;
        }

        private void OnChanges(object sender, FileSystemEventArgs e)
        {
            _outputStrategy.ScanAndSave(_device, e.FullPath, DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss fff"));
        }

        public async Task ScanExecute(string path, string name)
        {
           await _outputStrategy.ScanAndSave(_device, path, name);
        }
    }
}
