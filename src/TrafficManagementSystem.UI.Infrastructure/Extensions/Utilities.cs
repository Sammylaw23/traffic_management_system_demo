using MudBlazor;
using TrafficManagementSystem.Application.Wrappers;

namespace TrafficManagementSystem.UI.Infrastructure.Extensions
{
    public static class Utilities
    {
        public static void ShowFailureMessages(this IResponse response, ISnackbar snackbar)
        {
            response.Messages.ForEach(m => snackbar.Add(m, Severity.Error));
        }
    }
}
