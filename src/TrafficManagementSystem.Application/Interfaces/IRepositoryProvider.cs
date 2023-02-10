using TrafficManagementSystem.Application.Interfaces.Repositories;

namespace TrafficManagementSystem.Application.Interfaces
{
    public interface IRepositoryProvider
    {
        IDriverRepository DriverRepository { get; }
        IOffenceRepository OffenceRepository { get; }
        IOffenceTypeRepository OffenceTypeRepository { get; }
        IVehicleRepository VehicleRepository { get; }

        Task SaveChangesAsync();
    }
}
