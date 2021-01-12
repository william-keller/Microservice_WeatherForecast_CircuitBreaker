using Domain.ServiceClients.Base;
using Infrastructure.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.ServiceClient.Base
{
    public class BaseClient : IBaseClient
    {
        private readonly string _baseUri;
        private readonly HttpClient _client;

        public BaseClient(HttpClient client, string baseUri)
        {
            _client = client;
            _baseUri = baseUri;
        }

        public async Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken)
        {
            var result = await _client.GetAsync(uri, cancellationToken);
            
            result.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
        }

        public Uri BuildUri(string format)
        {
            var builder = new UriBuilder(_baseUri)
            {
                Path = format
            };

            NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

            query["success"] = TestHelper.MustFail.ToString();

            builder.Query = query.ToString();

            return builder.Uri;
        }
    }
}
