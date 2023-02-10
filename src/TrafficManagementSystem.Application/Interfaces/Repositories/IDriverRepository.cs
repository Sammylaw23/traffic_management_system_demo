using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Application.Interfaces.Repositories
{
    public interface IDriverRepository
    {
        Task AddDriverAsync(Driver driver);
        void UpdateDriver(Driver driver);
        Task<Driver> GetDriverByIdAsync(Guid? id);
        Task<IEnumerable<Driver>> GetDriversAsync();
        Task<Driver> GetDriverByLicenseNoAsync(string licenseNo);
        void DeleteDriver(Driver driver);
        Task<bool> LicenseIsValid(string licenseNumber);
    }
}
