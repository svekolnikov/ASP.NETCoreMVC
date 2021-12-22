
using System.IO.Compression;
using ScannerEmulator.Device;
using ScannerEmulator.Logger;

namespace ScannerEmulator.Strategies
{
    public class ZipScanOutputStrategy : IScanOutputStrategy
    {
        private readonly IScannerLogger _logger;

        public ZipScanOutputStrategy(IScannerLogger logger) => _logger = logger;

        public string Name { get; set; } = "ZIP strategy";

        public async Task ScanAndSave(IDevice device, string path, string outputName)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(nameof(path));
            if (string.IsNullOrWhiteSpace(outputName)) throw new ArgumentNullException(nameof(outputName));

            var outputPath = outputName + ".zip";
            if (File.Exists(outputPath)) File.Delete(outputPath);

            //Scan
            _logger.WriteInfo($"Start scanning: {path}");
            var streamIn = device.Scan(path);

            //Save
            _logger.WriteInfo($"Start saving to zip: {outputPath}");

            await using var zipFileStream = new FileStream(outputPath, FileMode.Create);
            using var archive = new ZipArchive(zipFileStream, ZipArchiveMode.Create);
            var entry = archive.CreateEntry(outputName + ".txt");
            await using var entryStream = entry.Open();

            streamIn.Position = 0;
            await streamIn.CopyToAsync(entryStream);
        }
    }
}
