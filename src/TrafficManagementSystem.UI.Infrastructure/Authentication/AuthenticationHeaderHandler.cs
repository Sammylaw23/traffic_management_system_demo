using Blazored.LocalStorage;
using System.Net.Http.Headers;
using TrafficManagementSystem.UI.Infrastructure.Constants;

namespace TrafficManagementSystem.UI.Infrastructure.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _storage;

        public AuthenticationHeaderHandler(ILocalStorageService storage)
            => _storage = storage;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != "Bearer")
            {
                var savedToken = await _storage.GetItemAsync<string>(AppConstants.Storage.AuthToken);
                if (!string.IsNullOrWhiteSpace(savedToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
