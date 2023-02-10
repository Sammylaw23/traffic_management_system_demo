using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TrafficManagementSystem.UI.Infrastructure.Extensions;

namespace TrafficManagementSystem.UI.Shared
{
    public partial class AuthorizedLayout
    {

        //AuthenticationState _authenticationState { get; set; }
        private CancellationTokenSource cts = new();

        [Parameter]
        public RenderFragment ChildContent { get; set; }
        public HubConnection HubConnection { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //_authenticationState = await _stateProvider.GetAuthenticationStateAsync();
            //_appStateManager.CurrentUsername =  _authenticationState.Username();
            //_httpInterceptor.RegisterEvent();

            HubConnection=HubConnection.TryInitialize(configuration, localStorage);
            //#pragma warning disable CS4014
            //            HubConnection.ConnectWithRetryAsync(cts.Token);
            //            HubConnection.On<string>(ApplicationConstants.SignalR.ConnectUser, (userId) =>
            //            {
            //                _snackbar.Add($"{userId} {_localizer["Logged In."]}", Severity.Info);
            //                StateHasChanged();
            //            });


            //            HubConnection.On(ApplicationConstants.SignalR.ReceiveRegenerateToken, async () =>
            //            {
            //                try
            //                {
            //                    await _authenticationManager.TryRefreshToken(true);

            //                }
            //                catch (Exception ex)
            //                {
            //#if DEBUG
            //                    Console.WriteLine(ex.Message);
            //#endif
            //                    _snackbar.Add("You are Logged Out.", Severity.Error);
            //                    await _authenticationManager.Logout();
            //                }
            //            });

            HubConnection.Closed += error => HubConnection.ConnectWithRetryAsync(cts.Token);
        }

        public async ValueTask DisposeAsync()
        {
            if (HubConnection is not null)
            {
                await HubConnection.DisposeAsync();
            }
        }


    }
}
