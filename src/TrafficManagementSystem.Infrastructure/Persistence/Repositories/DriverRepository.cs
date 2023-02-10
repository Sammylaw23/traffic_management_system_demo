using Microsoft.EntityFrameworkCore;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Repositories;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Infrastructure.Persistence.Repositories
{
    public class DriverRepository : RepositoryBase<Driver>, IDriverRepository
    {
        public DriverRepository(IApplicationDbContext context) : base(context)
        {
        }

        public async Task AddDriverAsync(Driver driver) => await AddAsync(driver);

        public async Task<Driver> GetDriverByIdAsync(Guid? id) => await GetByIdAsync(id);
        public async Task<IEnumerable<Driver>> GetDriversAsync() => await Get().ToListAsync();

        public void UpdateDriver(Driver driver) => Update(driver);
        public async Task<Driver> GetDriverByLicenseNoAsync(string licenseNo)
            => await _dbContext.Drivers.FirstOrDefaultAsync(d => d.LicenseNo == licenseNo);

        public void DeleteDriver(Driver driver) => Delete(driver);

        public async Task<bool> LicenseIsValid(string licenseNumber) => await _dbContext.Drivers.AnyAsync(x => x.LicenseNo == licenseNumber);

    }
}

