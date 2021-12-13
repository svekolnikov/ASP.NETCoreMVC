using System.Threading.Tasks;
using WeatherForecastWpf.Model;

namespace WeatherForecastWpf.Data
{
    public interface ITarget
    {
        public Task<WeatherForecastModel> GetRequest(string url);
    }
}
