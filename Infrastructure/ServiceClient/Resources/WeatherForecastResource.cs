using Domain.Entities;
using Domain.ServiceClients.Base;
using Domain.ServiceClients.Resources;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.ServiceClient.Resources
{
    public class WeatherForecastResource : IWeatherForecastResource
    {
        private readonly IBaseClient _client;

        public WeatherForecastResource(IBaseClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll(CancellationToken cancellationToken = default)
        {
            var uri = BuildUri();
            return await _client.GetAsync<IEnumerable<WeatherForecast>>(uri, cancellationToken);
        }

        private Uri BuildUri(string path = "")
        {
            return _client.BuildUri(string.Format("weatherforecast", path));
        }
    }
}
