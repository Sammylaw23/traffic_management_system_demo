using Microsoft.EntityFrameworkCore;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Repositories;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Infrastructure.Persistence.Repositories
{
    public class OffenceRepository : RepositoryBase<Offence>, IOffenceRepository
    {
        public OffenceRepository(IApplicationDbContext context) : base(context)
        {
        }

        public async Task AddOffenceAsync(Offence offence) => await AddAsync(offence);

        public async Task<Offence> GetOffenceByIdAsync(Guid? id) => await GetByIdAsync(id);

        public async Task<IEnumerable<Offence>> GetOffencesAsync() => await Get().ToListAsync();

        public void UpdateOffence(Offence offence) => Update(offence);
        public void DeleteOffence(Offence offence) => Delete(offence);

        public async Task<Offence> GetOffenceByLicenseNumberAsync(string licenseNo)
            => await _dbContext.Offences.FirstOrDefaultAsync(x => x.LicenseNo == licenseNo);

    }
}
