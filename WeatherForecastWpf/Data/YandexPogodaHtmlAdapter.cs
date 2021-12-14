using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using WeatherForecastWpf.Model;

namespace WeatherForecastWpf.Data
{
    public class YandexPogodaHtmlAdapter : ITarget
    {
        private readonly WeatherForecastData _weatherForecastData;
        private readonly HtmlParser _parser = new();
        private IHtmlDocument? _document;

        public YandexPogodaHtmlAdapter(WeatherForecastData weatherForecastData)
        {
            _weatherForecastData = weatherForecastData;
        }

        public async Task<WeatherForecastModel> GetRequest(string url)
        {
            var response = await _weatherForecastData.Get(url);

            _document = _parser.ParseDocument(response);

            var weatherForecastModel = new WeatherForecastModel();

            weatherForecastModel.Location = _document.GetElementById("main_title")?.InnerHtml;

            weatherForecastModel.CurrentTime = _document.QuerySelector(".fact__time")?.InnerHtml;

            var factTempDiv = _document.QuerySelector(".fact__temp");
            if (factTempDiv != null)
                weatherForecastModel.CurrentTemperature = factTempDiv.QuerySelector(".temp__value")?.InnerHtml;

            var feelsTempDiv = _document.QuerySelector(".fact__feels-like");
            if (feelsTempDiv != null)
                weatherForecastModel.FeelsTemperature = feelsTempDiv.QuerySelector(".temp__value")?.InnerHtml;

            var factYesterdayTempDiv = _document.QuerySelector(".fact__yesterday");
            if (factYesterdayTempDiv != null)
                weatherForecastModel.YesterdayTemperature = factYesterdayTempDiv.QuerySelector(".temp__value")?.InnerHtml;

            // Month weather forecast
            var daysContainer = _document
                .QuerySelectorAll(".forecast-briefly__days")[0]
                .Children[0];
            var days = daysContainer
                .QuerySelectorAll(".forecast-briefly__day");

            foreach (var dayDiv in days)
            {
                var name = dayDiv.QuerySelector(".forecast-briefly__name")?.InnerHtml;

                var date = dayDiv.QuerySelector(".forecast-briefly__date")?.InnerHtml;

                var dayTemperature = dayDiv.QuerySelector(".forecast-briefly__temp_day")?
                    .ChildNodes.QuerySelector(".temp__value_with-unit")?.InnerHtml;

                var nightTemperature = dayDiv.QuerySelector(".forecast-briefly__temp_night")?
                    .ChildNodes.QuerySelector(".temp__value_with-unit")?.InnerHtml;

                var condition = dayDiv.QuerySelector(".forecast-briefly__condition")?.InnerHtml;

                var iconSrc = "https:" + dayDiv.QuerySelector(".forecast-briefly__icon")?.GetAttribute("src");

                var dayOfWeek = dayDiv.ClassList.Contains("forecast-briefly__day_weekend");
                var day = dayOfWeek
                    ? Day.Weekend
                    : Day.Weekday;

                weatherForecastModel.ForecastBrieflyDay.Add(new()
                {
                    Name = name,
                    Date = date,
                    DayTemperature = dayTemperature,
                    NightTemperature = nightTemperature,
                    Condition = condition,
                    IconSrc = iconSrc,
                    Day = day
                });
            }

            return weatherForecastModel;
         }

        
    }
}
