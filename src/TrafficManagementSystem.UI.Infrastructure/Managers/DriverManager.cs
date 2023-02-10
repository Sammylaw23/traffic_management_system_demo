using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TrafficManagementSystem.Application.DTOs.Driver;
using TrafficManagementSystem.Application.Wrappers;
using TrafficManagementSystem.UI.Infrastructure.Constants;
using TrafficManagementSystem.UI.Infrastructure.Extensions;

namespace TrafficManagementSystem.UI.Infrastructure.Managers
{
    public interface IDriverManager
    {
        Task<IResponse> AddDriver(NewDriverRequest request);
        Task<List<DriverDto>> GetDrivers();
        Task<DriverDto> GetDriver(Guid id);
        Task<IResponse> DeleteDriver(Guid id);
    }
    public class DriverManager : IDriverManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DriverManager> _logger;
        private readonly ISnackbar _snackbar;

        public DriverManager(HttpClient httpClient, ILogger<DriverManager> logger, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _logger = logger;
            _snackbar = snackbar;
        }

        public async Task<IResponse> AddDriver(NewDriverRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoints.DriverEndpoints.AddDriver, request);
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string>>();
            return await Response.FailAsync(content.Messages);
        }

        public async Task<IResponse> DeleteDriver(Guid id)
        {
            var response = await _httpClient.DeleteAsync(Endpoints.DriverEndpoints.DeleteDriver(id));
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string>>();
            return await Response.FailAsync(content.Messages);
        }

        public async Task<DriverDto> GetDriver(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<Response<DriverDto>>(Endpoints.DriverEndpoints.GetDriver(id));
            if (response != null)
                return response.Data;
            return new DriverDto();
        }

        public async Task<List<DriverDto>> GetDrivers()
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<Response<List<DriverDto>>>(Endpoints.DriverEndpoints.GetDrivers)).Data;
            }
            catch (Exception ex)
            {
                _snackbar.Add("Failed to fetch drivers.", Severity.Error);
                _logger.LogError(ex.Format());
                return new List<DriverDto>();
            }
        }
    }
}
