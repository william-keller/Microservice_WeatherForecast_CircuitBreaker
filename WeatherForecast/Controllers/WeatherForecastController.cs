using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public ActionResult<IEnumerable<Domain.Entities.WeatherForecast>> Get([FromQuery] bool success = true)
        {
            if (!success) 
            {
                Console.WriteLine($" ###  BAD REQUEST  ### {DateTime.Now:HH:mm:ss}");
                return BadRequest();
            }

            Console.WriteLine($" ###  OK - 200     ### {DateTime.Now:HH:mm:ss}");

            var rng = new Random();

            var items = Enumerable.Range(1, 5).Select(index => new Domain.Entities.WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();

            return items;
        }
    }
}
