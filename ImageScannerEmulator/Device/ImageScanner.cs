using System.Diagnostics;

namespace ImageScannerEmulator.Device
{
    public class ImageScanner : IDevice
    {
        public async Task<byte[]> Scan(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();

            byte[] buffer;
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                buffer = new byte[fileStream.Length];

                await fileStream.ReadAsync(buffer, 0, (int) fileStream.Length);
            }
            finally
            {
                fileStream.Close();
            }

            return buffer;
        }
    }
}
