using HarbourControl.Models.OpenWeather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HarbourControl.WeatherService
{
    public interface IWeatherService
    {
        Task<Wind> GetHarbourWindSpeed();
    }
}
