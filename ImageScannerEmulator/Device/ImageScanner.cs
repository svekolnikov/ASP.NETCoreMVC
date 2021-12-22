using System.Diagnostics;

namespace ImageScannerEmulator.Device
{
    public class ImageScanner : IDevice
    {
        public async Task<byte[]> Scan(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();

            var fileInfo = new FileInfo(path);
            while (IsFileLocked(fileInfo)){}    // wait release process

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

        private static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using var stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
