using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace Infrastructure.Extensions.Policies
{
    public static class WeatherForecastServicePolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode != System.Net.HttpStatusCode.OK)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(3), OnRetry);
        }

        private static void OnRetry(DelegateResult<HttpResponseMessage> exception, TimeSpan timeSpan, Context context)
        {
            Console.WriteLine("###  Retrying  ###");
        }

        public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode != System.Net.HttpStatusCode.OK)
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(20));
        }

    }
}
