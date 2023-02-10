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
using TrafficManagementSystem.Application.DTOs.OffenceType;

namespace TrafficManagementSystem.UI.Infrastructure.Managers
{
    public interface IOffenceTypeManager
    {
        Task<IResponse> AddOffenceType(NewOffenceTypeRequest request);
        Task<List<OffenceTypeDto>> GetOffenceTypes();
        Task<OffenceTypeDto> GetOffenceType(Guid id);
        Task<IResponse> DeleteOffenceType(Guid id);
    }
    public class OffenceTypeManager : IOffenceTypeManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OffenceTypeManager> _logger;
        private readonly ISnackbar _snackbar;

        public OffenceTypeManager(HttpClient httpClient, ILogger<OffenceTypeManager> logger, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _logger = logger;
            _snackbar = snackbar;
        }

        public async Task<IResponse> AddOffenceType(NewOffenceTypeRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoints.OffenceTypeEndpoints.AddOffenceType, request);
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string>>();
            return await Response.FailAsync(content.Messages);
        }


        public async Task<IResponse> DeleteOffenceType(Guid id)
        {
            var response = await _httpClient.DeleteAsync(Endpoints.OffenceTypeEndpoints.DeleteOffenceType(id));
            if (response.IsSuccessStatusCode)
                return await Response.SuccessAsync();
            var content = await response.Content.ReadFromJsonAsync<Response<string>>();
            return await Response.FailAsync(content.Messages);
        }

        public async Task<OffenceTypeDto> GetOffenceType(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<Response<OffenceTypeDto>>(Endpoints.OffenceTypeEndpoints.GetOffenceType(id));
            if (response != null)
                return response.Data;
            return new OffenceTypeDto();
        }

        public async Task<List<OffenceTypeDto>> GetOffenceTypes()
        {
            try
            {
                return (await _httpClient.GetFromJsonAsync<Response<List<OffenceTypeDto>>>(Endpoints.OffenceTypeEndpoints.GetOffenceTypes)).Data;
            }
            catch (Exception ex)
            {
                _snackbar.Add("Failed to fetch Offence Types.", Severity.Error);
                _logger.LogError(ex.Format());
                return new List<OffenceTypeDto>();
            }
        }
    }

}
