using MudBlazor;
using TrafficManagementSystem.Application.DTOs.Vehicle;
using TrafficManagementSystem.UI.Components;
using TrafficManagementSystem.UI.Infrastructure.Extensions;

namespace TrafficManagementSystem.UI.Pages
{
    public partial class Vehicles
    {
        List<VehicleDto> vehicles = new();
        string? searchString;

        private VehicleDto selectedItem1 = null;

        private bool FilterFunc1(VehicleDto element) => FilterFunc(element, searchString);

        private bool FilterFunc(VehicleDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Owner.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.PlateNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Model.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.EngineNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Colour.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.ChassisNo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            //if ($"{element.Brand} {element.Model}".Contains(searchString))
            //    return true;
            return false;
        }

        async Task GetVehicles()
        {
            vehicles = await _vehicleManager.GetVehicles();
        }
        protected override async Task OnInitializedAsync()
        {
            await GetVehicles();
        }
        async Task InvokeVehicleDialog(Guid? id = null)
        {
            var parameters = new DialogParameters();
            if (id != null)
            {
                var driver = vehicles.FirstOrDefault(d => d.Id == id.Value);
                if (driver != null)
                {
                    parameters.Add(nameof(VehicleDialog.VehicleModel), new NewVehicleRequest
                    {
                        Id = driver.Id,
                        Name = driver.Name,
                        Owner = driver.Owner,
                        PlateNumber = driver.PlateNumber,
                        RegistrationDate =  driver.RegistrationDate,
                        Brand = driver.Brand,
                        ChassisNo = driver.ChassisNo,
                        Colour = driver.Colour,
                        EngineNumber = driver.EngineNumber,
                        Model = driver.Model,
                        Type = driver.Type
                    });
                }
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<VehicleDialog>("Vehicle Dialog", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetVehicles();
                StateHasChanged();
            }

        }

        async Task DeleteVehicle(VehicleDto vehicle)
        {
            string deleteContent = $"Delete {vehicle.Brand} vehicle owned by {vehicle.Owner} with plate number: {vehicle.PlateNumber}?";
            var parameters = new DialogParameters
            {
                { nameof(DeleteConfirmationDialog.ContentText), deleteContent }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await _vehicleManager.DeleteVehicle(vehicle.Id);
                if (response.Succeeded)
                {
                    _snackbar.Add("Vehicle record has been deleted successfully.", Severity.Success);
                    await GetVehicles();
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
