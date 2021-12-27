using Autofac;
using ImageScannerEmulator.Context;
using ImageScannerEmulator.Device;
using ImageScannerEmulator.Logger;
using ImageScannerEmulator.Strategies;
using Lesson6;


var builder = new ContainerBuilder();
builder.RegisterType<ImageScanner>().As<IDevice>().SingleInstance();
builder.RegisterType<ScannerConsoleLogger>().As<IScannerLogger>().SingleInstance();
builder.RegisterType<CropTwiceOutput>().As<IScanOutputStrategy>().SingleInstance();
builder.RegisterType<GrayscaleOutput>().As<IScanOutputStrategy>().SingleInstance();
builder.RegisterType<ImageScannerContext>().As<IScannerContext>().SingleInstance();
builder.RegisterType<Emulator>().As<IEmulator>().SingleInstance();
var container = builder.Build();

var emulator = container.Resolve<IEmulator>();

await emulator.Run();
Console.ReadLine();

emulator.Stop();
Console.ReadLine();