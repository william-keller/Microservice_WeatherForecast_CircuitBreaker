using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.ServiceClients.Resources
{
    public interface IWeatherForecastResource
    {
        Task<IEnumerable<WeatherForecast>> GetAll(CancellationToken cancellationToken = default);
    }
}
