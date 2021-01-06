using Domain.Entities;
using Domain.ServiceClients;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IWeatherForecastClient _weatherForecastClient;

        public WeatherForecastService(IWeatherForecastClient weatherForecastClient)
        {
            _weatherForecastClient = weatherForecastClient;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                return await _weatherForecastClient.WeatherForecastResource.GetAll(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
