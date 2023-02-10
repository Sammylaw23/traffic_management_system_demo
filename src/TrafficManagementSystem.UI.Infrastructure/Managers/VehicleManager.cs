using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficManagementSystem.Application.Wrappers;
using Microsoft.Extensions.Logging;
using MudBlazor;
using Microsoft.Extensions.Localization;
using System.Net.Http.Json;
using TrafficManagementSystem.UI.Infrastructure.Constants;
using TrafficManagementSystem.UI.Infrastructure.Extensions;

using TrafficManagementSystem.Application.DTOs.Vehicle;

namespace TrafficManagementSystem.UI.Infrastructure.Managers
{

    public interface IVehicleManager
    {
        Task<IResponse> AddVehicle(NewVehicleRequest request);
        Task<List<VehicleDto>> GetVehicles();
        Task<VehicleDto> GetVehicle(Guid id);
        Task<IResponse> DeleteVehicle(Guid id);
    }
    public class VehicleManager : IVehicleManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<VehicleManager> _logger;
        private readonly ISnackbar _snackbar;

        public VehicleManager(HttpClient httpClient, ILogger<VehicleManager> logger, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _logger = logger;
            _snackbar = snackbar;
        }

        public async Task<IResponse> AddVehicle(NewVehicleRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoints.VehicleEndpoints.AddVehicle, request);
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string>>();
            return await Response.FailAsync(content.Messages);
        }

        public async Task<IResponse> DeleteVehicle(Guid id)
        {
            var response = await _httpClient.DeleteAsync(Endpoints.VehicleEndpoints.DeleteVehicle(id));
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string?>>();
            return await Response.FailAsync(content.Messages);
        }

        public async Task<VehicleDto> GetVehicle(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<Response<VehicleDto>>(Endpoints.VehicleEndpoints.GetVehicle(id));
            return response == null ? new VehicleDto() : response.Data!;
        }

        public async Task<List<VehicleDto>> GetVehicles()
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<Response<List<VehicleDto>>>(Endpoints.VehicleEndpoints.GetVehicles)).Data!;
            }
            catch (Exception ex)
            {
                _snackbar.Add("Failed to fetch Vehicles.", Severity.Error);
                _logger.LogError(ex.Format());
                return new List<VehicleDto>();
            }
        }
    }

}
