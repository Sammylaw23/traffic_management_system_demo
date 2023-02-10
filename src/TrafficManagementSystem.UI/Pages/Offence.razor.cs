using MudBlazor;
using TrafficManagementSystem.Application.DTOs.Offence;
using TrafficManagementSystem.Application.DTOs.OffenceType;
using TrafficManagementSystem.UI.Components;
using TrafficManagementSystem.UI.Infrastructure.Extensions;

namespace TrafficManagementSystem.UI.Pages
{
    public partial class Offence
    {
        List<OffenceDto> offences = new();
        string? searchString;

        private OffenceDto selectedItem1 = null;

        private bool FilterFunc1(OffenceDto element) => FilterFunc(element, searchString);

        private bool FilterFunc(OffenceDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.OffenderName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.OffenceTypeCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.PlateNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.LicenseNo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.CreatedBy.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            
            return false;
        }
        protected override async Task OnInitializedAsync()
        {
            await GetOffences();
        }

        async Task GetOffences()
        {
            offences = await _offenceManager.GetOffences();
        }
        async Task InvokeOffenceDialog(Guid? id = null)
        {
            var parameters = new DialogParameters();
            if (id != null)
            {
                var offence = offences.FirstOrDefault(d => d.Id == id.Value);
                if (offence != null)
                {
                    parameters.Add(nameof(OffenceDialog.OffenceModel), new NewOffenceRequest
                    {
                        Id = offence.Id,
                        OffenceTypeCode = offence.OffenceTypeCode,
                        OffenderName = offence.OffenderName,
                        PlateNumber = offence.PlateNumber,
                        LicenseNo = offence.LicenseNo,
                    });
                }
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<OffenceDialog>("Offence Dialog", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetOffences();
                StateHasChanged();
            }


        }

        async Task DeleteOffence(OffenceDto offence)
        {
            string deleteContent = $"Delete offence committed by {offence.OffenderName} on {offence.CreatedTime}?";
            var parameters = new DialogParameters
            {
                { nameof(DeleteConfirmationDialog.ContentText), deleteContent }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await _offenceManager.DeleteOffence(offence.Id);
                if (response.Succeeded)
                {
                    _snackbar.Add("Offence record has been deleted successfully.", Severity.Success);
                    await GetOffences();
                    StateHasChanged();
                }
                else
                {
                    response.ShowFailureMessages(_snackbar);
                }
            }
        }

    }
}
