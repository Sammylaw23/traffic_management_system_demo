using TrafficManagementSystem.Application.DTOs.Vehicle;
using TrafficManagementSystem.Application.Wrappers;

namespace TrafficManagementSystem.Application.Interfaces.Services
{
    public interface IVehicleService
    {
        Task SaveVehicleAsync(NewVehicleRequest request);
        Task<Response<VehicleDto>> GetVehicleById(Guid id);
        Task<Response<IEnumerable<VehicleDto>>> GetAllVehicles();
        Task DeleteVehicleAsync(Guid id);
    }
}
