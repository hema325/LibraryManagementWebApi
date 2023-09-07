using Infrastructure.Identity.Models;
using Infrastructure.MultiTenancy.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    internal class ApplicationDbContextInitialiser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly TenancySettings _tenancySettings;

        public ApplicationDbContextInitialiser(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, IOptions<TenancySettings> tenancySettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tenancySettings = tenancySettings.Value;
        }

        public async Task InitialiseAsync()
        {
            if (_tenancySettings.IsEnabled)
            {
                foreach (var tenant in _tenancySettings.Tenants)
                {
                    _context.Database.SetConnectionString(tenant.ConnectionString);
                    await _context.Database.MigrateAsync();
                    await InitialiseIdentityAsync();
                    _context.ChangeTracker.Clear();
                }
            }
            else
            {
                await _context.Database.MigrateAsync();
                await InitialiseIdentityAsync();
            }
        }

        private async Task InitialiseIdentityAsync()
        {
            var roles = Enum.GetValues<Roles>()
                .Select(r => new ApplicationRole { Name = r.ToString() });

            if (await _roleManager.FindByNameAsync(roles.ElementAt(0).Name!) == null)
            {
                foreach (var role in roles)
                    await _roleManager.CreateAsync(role);
            }

            var user = new ApplicationUser
            {
                UserName = "Manager",
                Permissions = Enum.GetValues<Permissions>()
                .Select(p => new Permission { Name = p.ToString() }).ToList()
            };

            if (await _userManager.FindByNameAsync(user.UserName) == null)
            {
                await _userManager.CreateAsync(user, "P@$$w0rd");
                await _userManager.AddToRoleAsync(user, Roles.Manager.ToString());
            }
        }
    }
}
