using System.Collections.Generic;

namespace WeatherForecastWpf.Model
{
    public class WeatherForecastModel
    {
        public string? Location { get; set; }
        public string? CurrentTime { get; set; }
        public string? YesterdayTemperature { get; set; }
        public string? CurrentTemperature { get; set; }
        public string? FeelsTemperature { get; set; }
        public List<ForecastBrieflyDayModel> ForecastBrieflyDay { get; set; } = new List<ForecastBrieflyDayModel>();
    }
}
