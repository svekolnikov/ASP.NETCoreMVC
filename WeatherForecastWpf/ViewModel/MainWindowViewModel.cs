using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WeatherForecastWpf.Commands.Base;
using WeatherForecastWpf.Model;

namespace WeatherForecastWpf.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            WeatherForecasts = new ObservableCollection<WeatherForecastModel>
            {
                new() {DayNumber = "10"},
                new() {DayNumber = "11"},
                new() {DayNumber = "12"},
            };
        }

        private string _title;
        public string Title
        {
            get => _title;
            set 
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Search { get; set; }
        public ObservableCollection<WeatherForecastModel> WeatherForecasts { get; set; }

        #region GetWeatherCommand

        private Command? _getWeatherCommand;

        public ICommand GetWeatherCommand =>
            _getWeatherCommand ??= Command
                .Invoke(OnGetWeatherExecuted);

        private void OnGetWeatherExecuted(object? p)
        {
            MessageBox.Show("Search...");
        }

        #endregion
    }
}
