using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.ServiceClients.Base
{
    public interface IBaseClient
    {
        Task<T> GetAsync<T>(Uri uri, CancellationToken cancellationToken);
        Uri BuildUri(string format);
    }
}
