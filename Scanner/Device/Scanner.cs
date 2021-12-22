namespace ScannerEmulator.Device
{
    public class Scanner : IDevice
    {
        private FileStream? _fileStream;
        public FileStream Scan(string path)
        {
            if(!File.Exists(path)) throw new FileNotFoundException();

            _fileStream ??= new FileStream(path, FileMode.Open);

            return _fileStream;
        }
    }
}
