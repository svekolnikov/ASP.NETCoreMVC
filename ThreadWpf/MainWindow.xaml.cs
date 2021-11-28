using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ThreadWpf
{
    public partial class MainWindow : Window
    {
        private long _f = 0, _f1 = 1, _f2 = 1;
        private Thread _thread_1;
        private Thread _thread_2;
        private List<long> numbers;
        private bool _isRunning;
        private int _counter;
        public MainWindow()
        {         
            _thread_1 = new Thread(() => WorkerThread_1());
            _thread_1.Name = "Thread_1";
            _thread_1.IsBackground = true;            
            _thread_1.Start();

            _thread_2 = new Thread(() => WorkerThread_2());
            _thread_2.Name = "Thread_2";
            _thread_2.IsBackground = true;
            _thread_2.Start();

            InitializeComponent();

            _counter = 1000;
            Slider.Value = 1;
            LblCounter.Content = _counter;

            numbers = new List<long>();
        }

        private void WorkerThread_1()
        {
            while (true)
            {
                if (_isRunning)
                {
                    _ = Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        var number = NextFibonacci();
                        LblFiboncci.Content = number;
                        UiList.Items.Add($"Thread_1 added: {number}");
                        numbers.AddThreadSafe(number);
                    }, DispatcherPriority.Background);
                    
                    Thread.Sleep(_counter); //88. _thread_1.Interrupt(); -> System.Threading.ThreadInterruptedException: 'Thread was interrupted from a waiting state.'                   
                }                
            }
        }

        private void WorkerThread_2()
        {
            while (true)
            {
                if (_isRunning)
                {
                    _ = Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        var number = NextFibonacci();
                        LblFiboncci.Content = number;
                        UiList.Items.Add($"Thread_2 added: {number}");
                        numbers.AddThreadSafe(number);

                    }, DispatcherPriority.Background);

                    Thread.Sleep(_counter);
                }
            }
        }
        private long NextFibonacci()
        {
            _f = _f1 + _f2;
            _f2 = _f1;
            _f1 = _f;      
            return _f2;
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            _isRunning = !_isRunning;
            Btn.Content = _isRunning ? "Stop" : "Start";
            
            // _thread_1.Interrupt(); 
            // _thread.Abort();         // System.PlatformNotSupportedException: 'Thread abort is not supported on this platform.'
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _counter = (int)(Math.Round(e.NewValue,1) * 1000); // +100ms
            LblCounter.Content = _counter;
        }        
    }
}
