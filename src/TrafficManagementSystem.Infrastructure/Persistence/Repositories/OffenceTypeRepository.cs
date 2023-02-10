using Microsoft.EntityFrameworkCore;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Repositories;
using TrafficManagementSystem.Domain.Entities;

namespace TrafficManagementSystem.Infrastructure.Persistence.Repositories
{
    public class OffenceTypeRepository : RepositoryBase<OffenceType>, IOffenceTypeRepository
    {
        public OffenceTypeRepository(IApplicationDbContext context) : base(context)
        {
        }

        public async Task AddOffenceTypeAsync(OffenceType offenceType) => await AddAsync(offenceType);
        public async Task<OffenceType> GetOffenceTypeByIdAsync(Guid? id) => await GetByIdAsync(id);

        public async Task<IEnumerable<OffenceType>> GetOffenceTypesAsync() => await Get().ToListAsync();

        public void UpdateOffenceType(OffenceType offenceType) => Update(offenceType);
        public void DeleteOffenceType(OffenceType offenceType) => Delete(offenceType);

        public async Task<OffenceType> GetOffenceTypeByCodeAsync(string code)
            => await _dbContext.OffenceTypes.FirstOrDefaultAsync(x => x.Code == code);

        public async Task<IEnumerable<string>> GetOffenceTypeCodes() =>
            await _dbContext.OffenceTypes.Select(x => x.Code).ToListAsync();

    }
}
