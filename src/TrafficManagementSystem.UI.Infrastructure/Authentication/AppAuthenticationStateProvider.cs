using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using TrafficManagementSystem.Application.DTOs.User;
using TrafficManagementSystem.UI.Infrastructure.Constants;
using TrafficManagementSystem.UI.Infrastructure.Managers;

namespace TrafficManagementSystem.UI.Infrastructure.Authentication
{
    public class AppAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;
        private readonly AppStateManager _applicationStateManager;

        public AppAuthenticationStateProvider(
            ILocalStorageService localStorage,
            HttpClient httpClient,
            ISnackbar snackbar, AppStateManager applicationStateManager)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
            _snackbar = snackbar;
            _applicationStateManager=applicationStateManager;
        }

        public static AuthenticationState Anonymous { get; set; } = new(new ClaimsPrincipal(new ClaimsPrincipal()));

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync(AppConstants.Storage.AuthToken);
            if (string.IsNullOrEmpty(token))
            {
                _applicationStateManager.CurrentUsername=null;
                return Anonymous;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(GetClaims(token), "jwt"));
            _applicationStateManager.CurrentUsername = claimsPrincipal.FindFirst("username")?.Value;
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task NotifyAuthenticatedAsync(AuthenticationResponse response)
        {
            await _localStorage.SetItemAsync(AppConstants.Storage.AuthToken, response.JWToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task NotifyLogoutAsync()
        {
            await _localStorage.ClearAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
        }



        private static List<Claim> GetClaims(string jwtToken)
        {
            string payload = jwtToken.Split(".")[1];
            byte[] jsonBytes = ParseBase64StringWithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            List<Claim> claims = keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())).ToList();
            return claims;
        }

        private static byte[] ParseBase64StringWithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
