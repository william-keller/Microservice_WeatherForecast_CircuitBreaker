using Domain.ServiceClients;
using Domain.ServiceClients.Resources;
using Infrastructure.ServiceClient.Base;
using Infrastructure.ServiceClient.Resources;
using System.Net.Http;

namespace Infrastructure.ServiceClient
{
    public class WeatherForecastClient : IWeatherForecastClient
    {
        public IWeatherForecastResource WeatherForecastResource { get; }

        public WeatherForecastClient(HttpClient httpClient)
        {
            WeatherForecastResource = new WeatherForecastResource(new BaseClient(httpClient, httpClient.BaseAddress.ToString()));
        }
    }
}
