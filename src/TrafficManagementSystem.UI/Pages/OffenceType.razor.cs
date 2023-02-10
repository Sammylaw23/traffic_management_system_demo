using MudBlazor;
using TrafficManagementSystem.Application.DTOs.OffenceType;
using TrafficManagementSystem.UI.Components;
using TrafficManagementSystem.UI.Infrastructure.Extensions;

namespace TrafficManagementSystem.UI.Pages
{
    public partial class OffenceType
    {
        List<OffenceTypeDto> offenceTypes = new();
        string? searchString;

        private OffenceTypeDto selectedItem1 = null;

        private bool FilterFunc1(OffenceTypeDto element) => FilterFunc(element, searchString);

        private bool FilterFunc(OffenceTypeDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Code.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Category.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Point.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            
            return false;
        }
        protected override async Task OnInitializedAsync()
        {
            await GetOffenceTypes();
        }

        async Task GetOffenceTypes()
        {
            offenceTypes = await _offenceTypeManager.GetOffenceTypes();
        }

        async Task InvokeOffenceTypeDialog(Guid? id = null)
        {
            var parameters = new DialogParameters();
            if (id != null)
            {
                var offenceType = offenceTypes.FirstOrDefault(d => d.Id == id.Value);
                if (offenceType != null)
                {
                    parameters.Add(nameof(OffenceTypesDialog.OffenceTypeModel), new NewOffenceTypeRequest
                    {
                        Id = offenceType.Id,
                        Category = offenceType.Category,
                        Code = offenceType.Code,
                        FineAmount = offenceType.FineAmount,
                        Name = offenceType.Name,
                        Point = offenceType.Point
                    });
                }
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<OffenceTypesDialog>("OffenceType Dialog", parameters, options);
            var result = await dialog.Result;
          
            if (!result.Cancelled)
            {
                await GetOffenceTypes();
                StateHasChanged();
            }
        }

        async Task DeleteOffenceType(OffenceTypeDto offenceType)
        {
            string deleteContent = $"Delete offenceType ({offenceType.Name} Code({offenceType.Code}))?";
            var parameters = new DialogParameters
            {
                { nameof(DeleteConfirmationDialog.ContentText), deleteContent }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await _offenceTypeManager.DeleteOffenceType(offenceType.Id);
                if (response.Succeeded)
                {
                    _snackbar.Add("Offence-Type record has been deleted successfully.", Severity.Success);
                    await GetOffenceTypes();
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
