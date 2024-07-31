using Core.Domain.Persistence.Models.Common;
using Core.Domain.Persistence.SharedModels.General;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUser _authenticatedUser;
        public AppDbContext(DbContextOptions<AppDbContext> options, IAuthenticatedUser authenticatedUser, IDateTimeService dateTime)
            : base(options)
        {
            _authenticatedUser = authenticatedUser;
            _dateTime = dateTime;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Soft delete setup
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
            #endregion

            base.OnModelCreating(modelBuilder);
        }

        
        public async Task<int> SaveChangesAsync()
        {
            foreach(var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = _dateTime.Now;
                        entry.Entity.CreatedBy = entry.Entity.CreatedBy != null 
                            ? entry.Entity.CreatedBy 
                            : _authenticatedUser.UserId;

                        entry.Entity.ClientBusinessProfileId = entry.Entity.ClientBusinessProfileId != null 
                            ? entry.Entity.ClientBusinessProfileId 
                            : _authenticatedUser.ClientBusinessProfileId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdatedDate = _dateTime.Now;
                        entry.Entity.LastUpdatedBy = entry.Entity.LastUpdatedBy != null 
                            ? entry.Entity.LastUpdatedBy 
                            : _authenticatedUser.UserId;

                        entry.Entity.ClientBusinessProfileId = entry.Entity.ClientBusinessProfileId != null 
                            ? entry.Entity.ClientBusinessProfileId 
                            : _authenticatedUser.ClientBusinessProfileId;
                        break;
                }
            }
            return await base.SaveChangesAsync();
        }
    }
}
