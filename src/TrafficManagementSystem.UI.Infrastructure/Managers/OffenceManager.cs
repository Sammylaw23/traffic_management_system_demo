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
using TrafficManagementSystem.Application.DTOs.Offence;

namespace TrafficManagementSystem.UI.Infrastructure.Managers
{
    public interface IOffenceManager
    {
        Task<IResponse> AddOffence(NewOffenceRequest request);
        Task<List<OffenceDto>> GetOffences();
        Task<OffenceDto> GetOffence(Guid id);
        Task<IResponse> DeleteOffence(Guid id);
    }
    public class OffenceManager : IOffenceManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OffenceManager> _logger;
        private readonly ISnackbar _snackbar;

        public OffenceManager(HttpClient httpClient, ILogger<OffenceManager> logger, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _logger = logger;
            _snackbar = snackbar;
        }

        public async Task<IResponse> AddOffence(NewOffenceRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoints.OffenceEndpoints.AddOffence, request);
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string>>();
            return await Response.FailAsync(content.Messages);
        }

        public async Task<IResponse> DeleteOffence(Guid id)
        {
            var response = await _httpClient.DeleteAsync(Endpoints.OffenceEndpoints.DeleteOffence(id));
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string>>();
            return await Response.FailAsync(content.Messages);
        }

        public async Task<OffenceDto> GetOffence(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<Response<OffenceDto>>(Endpoints.OffenceEndpoints.GetOffence(id));
            if (response != null)
                return response.Data;
            return new OffenceDto();
        }

        public async Task<List<OffenceDto>> GetOffences()
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<Response<List<OffenceDto>>>(Endpoints.OffenceEndpoints.GetOffences)).Data;
            }
            catch (Exception ex)
            {
                _snackbar.Add("Failed to fetch Offences.", Severity.Error);
                _logger.LogError(ex.Format());
                return new List<OffenceDto>();
            }
        }
    }

}
