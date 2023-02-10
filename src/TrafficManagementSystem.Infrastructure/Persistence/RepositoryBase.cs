using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Infrastructure.DbContexts;

namespace TrafficManagementSystem.Infrastructure.Persistence
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected readonly TrafficManagementSystemDbContext _dbContext;

        public RepositoryBase(IApplicationDbContext context)
        {
            _dbContext = (TrafficManagementSystemDbContext)context;
        }

        protected virtual async Task AddAsync(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        protected virtual void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        protected virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
                query.Where(filter).AsNoTracking();

            foreach (string includedProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includedProperty);
            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        protected virtual async Task<T> GetByIdAsync(object id) => await _dbContext.Set<T>().FindAsync(id);

        protected virtual void Update(T entity) => _dbContext.Entry(entity).State = EntityState.Modified;

    }
}
