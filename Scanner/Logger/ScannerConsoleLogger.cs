namespace ScannerEmulator.Logger
{
    public class ScannerConsoleLogger : IScannerLogger
    {
        public void WriteInfo(string message)
        {
            Console.WriteLine($"[{DateTime.Now:T}] - {message}");
        }
    }
}
