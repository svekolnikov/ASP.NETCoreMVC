using ImageScannerEmulator.Device;
using ImageScannerEmulator.Logger;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ImageScannerEmulator.Strategies
{
    public class CropTwiceOutput : IScanOutputStrategy
    {
        protected readonly IScannerLogger _logger;
        public CropTwiceOutput(IScannerLogger logger) => _logger = logger;

        public async Task ScanAndSave(IDevice device, string sourcePath, string destName)
        {
            if (string.IsNullOrWhiteSpace(destName)) throw new ArgumentNullException(nameof(destName));

            //Scan
            _logger.WriteInfo($"Start scanning: {sourcePath}");
            var bytes = await device.Scan(sourcePath);

            //Save
            var currentPath = Directory.GetCurrentDirectory();
            var destDir = Path.Combine(currentPath, "scanned_images");

            if (!Directory.Exists(destDir)) Directory.CreateDirectory(destDir);

            var destPath = Path.Combine(destDir, destName + Path.GetExtension(sourcePath));

            _logger.WriteInfo($"Start saving to grayscale image: {destName}");

            var memStream = new MemoryStream();
            await memStream.WriteAsync(bytes, 0, bytes.Length);
            memStream.Position = 0;

            using var image = await Image.LoadAsync(memStream);
            image.Mutate(x => x.
                Resize(image.Width / 2, image.Height / 2));

            await image.SaveAsync(destPath);
        }
    }
}
