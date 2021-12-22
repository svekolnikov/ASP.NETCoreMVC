using ScannerEmulator.Device;
using ScannerEmulator.Logger;

namespace ScannerEmulator.Strategies
{
    public sealed class PdfScanOutputStrategy : IScanOutputStrategy
    {
        private readonly IScannerLogger _logger;
        public PdfScanOutputStrategy(IScannerLogger logger) => _logger = logger;

        public string Name { get; set; } = "PDF strategy";

        public async Task ScanAndSave(IDevice device, string path, string outputName)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(nameof(path));
            if (string.IsNullOrWhiteSpace(outputName)) throw new ArgumentNullException(nameof(outputName));

            var outputPath = outputName + ".pdf";
            if(File.Exists(outputPath)) File.Delete(outputPath);

            //Scan
            _logger.WriteInfo($"Start scanning: {path}");
            var streamIn = device.Scan(path);

            //Save
            _logger.WriteInfo($"Start saving to pdf: {outputPath}");
            await using var streamOut = new FileStream(outputPath, FileMode.Create);

            streamIn.Position = 0;
            await streamIn.CopyToAsync(streamOut);
        }
    }
}
