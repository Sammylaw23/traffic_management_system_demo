using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Application.Interfaces.Repositories
{
    public interface IOffenceRepository
    {
        Task AddOffenceAsync(Offence offence);
        void UpdateOffence(Offence offence);
        Task<Offence> GetOffenceByIdAsync(Guid? id);
        Task<IEnumerable<Offence>> GetOffencesAsync();
        void DeleteOffence(Offence offence);
        Task<Offence> GetOffenceByLicenseNumberAsync(string licenseNo);



    }
}
