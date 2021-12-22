namespace ScannerEmulator.Device
{
    public interface IDevice
    {
        FileStream Scan(string name);
    }
}
