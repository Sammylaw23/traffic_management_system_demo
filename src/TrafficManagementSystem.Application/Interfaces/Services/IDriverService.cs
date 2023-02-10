using TrafficManagementSystem.Application.DTOs.Driver;
using TrafficManagementSystem.Application.Wrappers;

namespace TrafficManagementSystem.Application.Interfaces.Services
{
    public interface IDriverService
    {
        Task SaveDriverAsync(NewDriverRequest request);
        Task<Response<DriverDto>> GetDriverById(Guid id);
        Task<Response<IEnumerable<DriverDto>>> GetAllDrivers();
        Task DeleteDriverAsync(Guid id);

    }
}
