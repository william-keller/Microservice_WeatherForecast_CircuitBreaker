using Domain.ServiceClients;
using Domain.Services;
using Infrastructure.Extensions.Policies;
using Infrastructure.ServiceClient;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Extensions
{
    public static class WeatherForecastServiceExtensions
    {
        public static IServiceCollection AddWeatherForecastService(this IServiceCollection services, Uri uri)
        {
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            services.AddHttpClient<IWeatherForecastClient, WeatherForecastClient>(client => { client.BaseAddress = uri; })
                .SetHandlerLifetime(TimeSpan.FromMinutes(2))
                //.AddPolicyHandler(WeatherForecastServicePolicies.RetryPolicy())
                .AddPolicyHandler(WeatherForecastServicePolicies.CircuitBreakerPolicy());

            return services;
        }
    }
}
