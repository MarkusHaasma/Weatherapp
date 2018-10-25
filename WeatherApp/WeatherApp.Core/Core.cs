using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string city)
        {
            string key = "a519d2565f58343b5f157d056e658aca";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&APPID=" +key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            try
            {
                var weather = new Weather();
                weather.Temperature = (string)results["main"]["temp"] + " C";
                weather.MinTemp = (string)results["main"]["temp_min"] + " C";
                weather.MaxTemp = (string)results["main"]["temp_max"] + " C";
                weather.Type = (string)results["weather"][0]["main"] + " ";
                weather.Pressure = (string)results["main"]["pressure"] + " ";
                weather.WindSpeed = (string)results["wind"]["speed"] + " ";
                weather.Icon = (string)results["weather"][0]["icon"] + " ";
                return weather;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
