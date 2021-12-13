namespace WeatherForecastWpf.Model
{
    public enum Day : byte
    {
        Weekday,
        Weekend
    }
    public class ForecastBrieflyDayModel
    {
        public string? Name { get; set; }
        public string? Date { get; set; }
        public string? DayTemperature { get; set; }
        public string? NightTemperature { get; set; }
        public string? Condition { get; set; }
        public string? IconSrc { get; set; }
        public Day Day { get; set; }
        public override string ToString()
        {
            return $"{Name} {Date}: Днём {DayTemperature}, Ночью {NightTemperature}, Условия {Condition}";
        }
    }
}
