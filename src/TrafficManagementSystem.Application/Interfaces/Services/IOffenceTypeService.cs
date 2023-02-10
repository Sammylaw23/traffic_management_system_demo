using TrafficManagementSystem.Application.DTOs.OffenceType;
using TrafficManagementSystem.Application.Wrappers;

namespace TrafficManagementSystem.Application.Interfaces.Services
{
    public interface IOffenceTypeService
    {
        Task SaveOffenceTypeAsync(NewOffenceTypeRequest request);
        Task<Response<OffenceTypeDto>> GetOffenceTypeById(Guid id);
        Task<Response<IEnumerable<OffenceTypeDto>>> GetAllOffenceTypes();
        Task DeleteOffenceTypeAsync(Guid id);

        Task<Response<IEnumerable<string>>> GetOffenceTypeCodes();


    }
}
