using MudBlazor;
using TrafficManagementSystem.Application.DTOs.Driver;
using TrafficManagementSystem.UI.Components;
using TrafficManagementSystem.UI.Infrastructure.Extensions;

namespace TrafficManagementSystem.UI.Pages
{
    public partial class Drivers
    {
        List<DriverDto> drivers = new();
        string? searchString;
        //MemoryStream excelStream;

        private DriverDto selectedItem1 = null;

        private bool FilterFunc1(DriverDto element) => FilterFunc(element, searchString);

        private bool FilterFunc(DriverDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.PhoneNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.LicenseNo.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.FirstName} {element.LastName}".Contains(searchString))
                return true;
            return false;
        }

        protected override async Task OnInitializedAsync()
        {
            await GetDrivers();
        }

        async Task GetDrivers()
        {
            drivers = await _driverManager.GetDrivers();
        }
        async Task InvokeDriverDialog(Guid? id = null)
        {
            var parameters = new DialogParameters();
            if (id != null)
            {
                var driver = drivers.FirstOrDefault(d => d.Id == id.Value);
                if (driver != null)
                {
                    parameters.Add(nameof(DriverDialog.DriverModel), new NewDriverRequest
                    {
                        Id = driver.Id,
                        DateOfBirth = driver.DateOfBirth,
                        Email = driver.Email,
                        FirstName = driver.FirstName,
                        LastName = driver.LastName,
                        Gender = driver.Gender,
                        LicenseNo = driver.LicenseNo,
                        Address = driver.Address,
                        PhoneNumber = driver.PhoneNumber
                    });
                }
            }

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DriverDialog>("Driver Dialog", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await GetDrivers();
                StateHasChanged();
            }

        }

        async Task DeleteDriver(DriverDto driver)
        {
            string deleteContent = $"Delete driver ({driver.FirstName} {driver.LastName})?";
            var parameters = new DialogParameters
            {
                { nameof(DeleteConfirmationDialog.ContentText), deleteContent }
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<DeleteConfirmationDialog>("Delete", parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await _driverManager.DeleteDriver(driver.Id.Value);
                if (response.Succeeded)
                {
                    _snackbar.Add("Driver record has been deleted successfully.", Severity.Success);
                    await GetDrivers();
                    StateHasChanged();
                }
                else
                {
                    response.ShowFailureMessages(_snackbar);
                }
            }
        }

        //async Task DownloadDriverExcel(List<DriverDto> drivers)
        //{
        //    //await _documentManager.DownloadExcel(drivers);
            
        //    excelStream = service.CreateExcel();
        //    await JS.SaveAs("Sample.xlsx", excelStream.ToArray());

        //}
    }
}



    