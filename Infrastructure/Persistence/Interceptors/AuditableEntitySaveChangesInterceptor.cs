using Domain.Common.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors
{
    internal class AuditableEntitySaveChangesInterceptor: SaveChangesInterceptor
    {
        private readonly ICurrentUser _currentUser;
        private readonly IDateTime _dateTime;

        public AuditableEntitySaveChangesInterceptor(ICurrentUser currentUser, IDateTime dateTime)
        {
            _currentUser = currentUser;
            _dateTime = dateTime;
        }


        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var entries = eventData.Context.ChangeTracker.Entries<AuditableEntity>();

            foreach(var entry in entries)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedOn = _dateTime.Now;
                    entry.Entity.CreatedBy = _currentUser.Id;
                    entry.Entity.ModifiedOn = _dateTime.Now;
                    entry.Entity.ModifiedBy = _currentUser.Id;
                }
                else if(entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedOn = _dateTime.Now;
                    entry.Entity.ModifiedBy = _currentUser.Id;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
