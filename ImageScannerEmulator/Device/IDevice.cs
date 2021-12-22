namespace ImageScannerEmulator.Device
{
    public interface IDevice
    {
        Task<byte[]> Scan(string name);
    }
}
