using System.Linq.Expressions;
using ImageScannerEmulator.Context;
using ImageScannerEmulator.Device;
using ImageScannerEmulator.Strategies;
using Lesson5;
using Lesson5.ExpressionTree;
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
var imgGrayscaleStrategy = new GrayscaleOutput(new ImageScannerEmulator.Logger.ScannerConsoleLogger());
var cropTwiceOutputStrategy = new CropTwiceOutput(new ImageScannerEmulator.Logger.ScannerConsoleLogger());
var imgScannerContext = new ImageScannerContext(imgScanner, cropTwiceOutputStrategy);
await imgScannerContext.ScanExecute("stm32.png", "gray_image_1");


imgScannerContext.WatchRun("watch_here");
//imgScannerContext.WatchStop();
Console.ReadLine();

//Sql expression
//Database
var users = Enumerable.Range(1, 10).Select(i => 
    new Customer
    {
        Id = i,
        Name = $"Customer-{i}",
        Orders = Enumerable.Range(1,5).Select(o => new Order
        {
            Id = o,
            Name = $"Order-{o}",
            Price = i*o
        }).ToList()
    }
);

//Linq query
var sqlExp = new SqlExpression();
var sql = sqlExp.SqlRequest(u => u.Where(x => x.Id > 5));
Console.WriteLine(sql);