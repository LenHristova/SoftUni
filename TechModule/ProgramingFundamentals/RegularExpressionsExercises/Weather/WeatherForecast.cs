namespace Weather
{
    class WeatherForecast
    {
        public string City { get; set; }
        public double AvarageTemperature { get; set; }
        public string Weather { get; set; }

        public override string ToString()
        {
            return $"{City} => {AvarageTemperature:F2} => {Weather}";
        }
    }
}