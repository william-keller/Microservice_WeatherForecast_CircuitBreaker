using Domain.ServiceClients.Resources;

namespace Domain.ServiceClients
{
    public interface IWeatherForecastClient
    {
        IWeatherForecastResource WeatherForecastResource { get; }
    }
}
