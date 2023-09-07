using Infrastructure.Common.Extensions;
using Infrastructure.Identity.Models;
using Infrastructure.MultiTenancy.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Infrastructure.Persistence
{
    internal class ApplicationDbContext: IdentityDbContext<ApplicationUser,ApplicationRole, int>, IApplicationDbContext
    {
        private readonly IPublisher _publisher;
        private readonly ICurrentTenant _currentTenant;
        private readonly TenancySettings _tenancySettings;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher, ICurrentTenant currentTenant, IOptions<TenancySettings> tenancySettings) : base(options)
        {
            _publisher = publisher;
            _currentTenant = currentTenant;
            _tenancySettings = tenancySettings.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (_tenancySettings.IsEnabled)
                optionsBuilder.UseSqlServer(_currentTenant.ConnectionString);
            else
                optionsBuilder.UseSqlServer("name=default");

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var affectedRows =  await base.SaveChangesAsync(cancellationToken);
            await _publisher.DispatchDomainEventsAsync(this);
            return affectedRows;
        }

        public DbSet<Category> Categories { get; private set; }
        public DbSet<Author> Authors { get; private set; }
        public DbSet<Book> Books { get; private set; }
        public DbSet<Gender> Genders { get; private set; }
        public DbSet<ClientStatus> ClientStatuses { get; private set; }
        public DbSet<Client> Clients { get; private set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; private set; }
        public DbSet<Reservation> Reservations { get; private set; }
        public DbSet<Loan> Loans { get; private set; }
        public DbSet<Fine> Fines { get; private set; }
        public DbSet<Payment> Payments { get; private set; }
       
        public DbSet<Permission> Permissions { get; private set; }
        public DbSet<ApplicationUser> Users { get; private set; }
        public DbSet<ApplicationRole> Roles { get; private set; }
    }
}
