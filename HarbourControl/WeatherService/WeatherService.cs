using HarbourControl.Models.OpenWeather;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HarbourControl.WeatherService
{
    public class WeatherService : IWeatherService
    {
        string url = "api.openweathermap.org/data/2.5/weather?q=durban&appid=f4f0a01dd170f927829865108b4b396d";

        HttpClient client;
        public WeatherService()
        {
            client = new HttpClient();
        }
        public async Task<Wind> GetHarbourWindSpeed()
        {
            var currentWind = new Wind();
            try
            {
                var uri = new Uri(url);

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.DeserializeObject<OpenWeather>(content);

                    currentWind = resp.wind;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return currentWind;
        }
    }
}
