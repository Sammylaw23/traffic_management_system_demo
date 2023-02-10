using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrafficManagementSystem.Application.Interfaces;
using TrafficManagementSystem.Application.Interfaces.Services;
using TrafficManagementSystem.Domain.Entities;
using TrafficManagementSystem.Infrastructure.Models;

namespace TrafficManagementSystem.Infrastructure.DbContexts
{
    public class TrafficManagementSystemDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public TrafficManagementSystemDbContext(DbContextOptions<TrafficManagementSystemDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService=currentUserService;
        }

        public DbSet<Driver>? Drivers { get; set; }
        public DbSet<OffenceType>? OffenceTypes { get; set; }
        public DbSet<Offence>? Offences { get; set; }
        public DbSet<Vehicle>? Vehicles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Username;
                        entry.Entity.CreatedTime = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var offenceType = modelBuilder.Entity<OffenceType>();
            offenceType.Property(x => x.FineAmount).HasPrecision(10, 3);

            //modelBuilder.Entity<Offence>()
            //   .HasOne(e => e.OffenceType)
            //   .WithMany(e => e.Offences);

            base.OnModelCreating(modelBuilder);
        }
    }
}
