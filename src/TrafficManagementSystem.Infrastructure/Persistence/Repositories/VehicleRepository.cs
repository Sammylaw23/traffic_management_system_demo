using Microsoft.EntityFrameworkCore;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Repositories;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(IApplicationDbContext context) : base(context)
        {

        }


        public async Task AddVehicleAsync(Vehicle vehicle) => await AddAsync(vehicle);

        public async Task<Vehicle> GetVehicleByIdAsync(Guid? id) => await GetByIdAsync(id);

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync() => await Get().ToListAsync();

        public void UpdateVehicle(Vehicle vehicle) => Update(vehicle);
        public void DeleteVehicle(Vehicle vehicle) => Delete(vehicle);

        public async Task<Vehicle> GetVehicleByPlateNumberAsync(string plateNumber)
            => await _dbContext.Vehicles.FirstOrDefaultAsync(d => d.PlateNumber == plateNumber);

        public async Task<bool> VehicleExists(Vehicle vehicle) =>
            await _dbContext.Vehicles.AnyAsync(x => x.PlateNumber == vehicle.PlateNumber || x.EngineNumber == vehicle.EngineNumber
            || x.ChassisNo == vehicle.ChassisNo);

        public async Task<bool> PlateNumberIsValid(string plateNumber) => await _dbContext.Vehicles.AnyAsync(x => x.PlateNumber == plateNumber);

    }
}


