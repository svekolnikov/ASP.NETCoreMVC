using ImageScannerEmulator.Context;
using ImageScannerEmulator.Device;
using ImageScannerEmulator.Strategies;
using ScannerEmulator.Context;
using ScannerEmulator.Device;
using ScannerEmulator.Logger;
using ScannerEmulator.Strategies;

Console.WriteLine("Start...");

var scanner = new Scanner();
var pdfStrategy = new PdfScanOutputStrategy(new ScannerConsoleLogger());

var scannerContext = new DefaultScannerContext(scanner, pdfStrategy);
await scannerContext.ScanExecute("text.txt", "scannedToPdf");

var zipStrategy = new ZipScanOutputStrategy(new ScannerConsoleLogger());
scannerContext.SetDevice(scanner);
scannerContext.SetStrategy(zipStrategy);
await scannerContext.ScanExecute("text.txt", "scannedToZip");

var imgStrategy = new ImageScanOutputStrategy(new ScannerConsoleLogger());
scannerContext.SetDevice(scanner);
scannerContext.SetStrategy(imgStrategy);
await scannerContext.ScanExecute("stm32.png", "scannedImage");

var imgScanner = new ImageScanner();
var imgGrayscaleStrategy = new GrayscaleScanOutputStrategy(new ImageScannerEmulator.Logger.ScannerConsoleLogger());
var imgScannerContext = new ImageScannerContext(imgScanner, imgGrayscaleStrategy);
await imgScannerContext.ScanExecute("stm32.png", "gray_image");
imgScannerContext.WatchRun("watch_here");
//imgScannerContext.WatchStop();
Console.ReadLine();



