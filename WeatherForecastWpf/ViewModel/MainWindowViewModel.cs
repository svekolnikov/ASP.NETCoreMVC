using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WeatherForecastWpf.Commands.Base;
using WeatherForecastWpf.Data;
using WeatherForecastWpf.Model;

namespace WeatherForecastWpf.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private string _title;
        private readonly Adapter _adapter;

        public MainWindowViewModel()
        {
            var weatherForecastData = new WeatherForecastData();
            _adapter = new Adapter(weatherForecastData);
        }

        public string Title
        {
            get => _title;
            set 
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        
        public ObservableCollection<ForecastBrieflyDayModel> WeatherForecasts { get; set; } = new ObservableCollection<ForecastBrieflyDayModel>();

        #region Get weather command

        private Command? _getWeatherCommand;

        public ICommand GetWeatherCommand =>
            _getWeatherCommand ??= Command.Invoke(OnGetWeatherExecuted);

        private async Task OnGetWeatherExecuted(object? p)
        {
            await Update();
        }

        #endregion

        #region Window loaded command

        private Command? _windowLoadedCommand;

        public ICommand WindowLoadedCommand =>
            _windowLoadedCommand ??= Command.Invoke(OnWindowLoadedExecuted);

        private async Task OnWindowLoadedExecuted(object? p)
        {
            await Update();
        }

        #endregion

        private async Task Update()
        {
            var model = await _adapter.GetRequest("https://yandex.ru/pogoda/");

            Title = $"{model.CurrentTime} " +
                    $"Моё местоположение: {model.Location}. " +
                    $"Текущая температура {model.CurrentTemperature} " +
                    $"Ощущается как {model.FeelsTemperature} " +
                    $"Вчера в это время {model.YesterdayTemperature}";

            WeatherForecasts.Clear();
            model.ForecastBrieflyDay.ForEach(x => WeatherForecasts.Add(x));
        }
    }
}
