using ImageScannerEmulator.Context;

namespace Lesson6
{
    public class Emulator : IEmulator
    {
        private readonly IScannerContext _scanner;

        public Emulator(IScannerContext scanner)
        {
            _scanner = scanner;
        }

        public async Task Run()
        {
            Console.WriteLine("Run...");
            await _scanner.ScanExecute("stm32.png", "gray_image_1");
            _scanner.WatchRun("watch_here");
            
        }

        public void Stop()
        {
            _scanner.WatchStop();
        }
    }
}
