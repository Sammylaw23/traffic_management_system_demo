using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net;
using Toolbelt.Blazor;
using TrafficManagementSystem.UI.Infrastructure.Authentication;
using TrafficManagementSystem.UI.Infrastructure.Constants;

namespace TrafficManagementSystem.UI.Infrastructure.Extensions
{
    public class ApiInterceptor
    {

        private readonly HttpClientInterceptor _interceptor;
        private readonly ISnackbar _snackbar;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ApiInterceptor(HttpClientInterceptor interceptor, ISnackbar snackbar, AuthenticationStateProvider authenticationStateProvider)
        {
            _interceptor = interceptor;
            _snackbar = snackbar;
            _authenticationStateProvider=authenticationStateProvider;
        }

        public void RegisterEvent()
        {
            _interceptor.AfterSendAsync += InterceptResponseAsync;
        }


        private async Task InterceptResponseAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            if (!e?.Response?.IsSuccessStatusCode ?? false)
            {
                switch (e!.Response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        _snackbar.Add(AppConstants.ErrorMessages.SessionTimeout, Severity.Error);
                        await ((AppAuthenticationStateProvider)_authenticationStateProvider).NotifyLogoutAsync();
                        break;
                }
            }
        }

        public void DisposeEvent()
        {
            _interceptor.AfterSendAsync -= InterceptResponseAsync;
        }
    }
}
