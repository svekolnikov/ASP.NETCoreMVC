using ScannerEmulator.Device;
using ScannerEmulator.Logger;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace ScannerEmulator.Strategies
{
    public class ImageScanOutputStrategy : IScanOutputStrategy
    {
        protected readonly IScannerLogger _logger;
        public ImageScanOutputStrategy(IScannerLogger logger) => _logger = logger;

        public string Name { get; set; } = "Image strategy";

        public async Task ScanAndSave(IDevice device, string path, string outputName)
        {
            if (!File.Exists(path)) throw new FileNotFoundException(nameof(path));
            if (string.IsNullOrWhiteSpace(outputName)) throw new ArgumentNullException(nameof(outputName));

            //Scan
            _logger.WriteInfo($"Start scanning: {path}");
            var streamIn = device.Scan(path);

            //Save
            var outputPath = $"{outputName}.png";
            _logger.WriteInfo($"Start saving to pdf: {outputPath}");

            using var image = await Image.LoadAsync(path);
            image.Mutate(x => x
                .Resize(image.Width / 2, image.Height / 2));

            await image.SaveAsync(outputPath);
        }
    }
}
