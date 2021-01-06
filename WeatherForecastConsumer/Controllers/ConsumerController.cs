using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherForecastConsumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public ConsumerController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAll(CancellationToken cancellationToken)
        {
            return await _weatherForecastService.GetAll(cancellationToken);
        }
    }
}
