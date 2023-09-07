using Infrastructure.Identity.Models;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    internal class IdentityService: IIdentity
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public IdentityService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<string?> GetUserNameById(int id)
        {
            return (await _userManager.FindByIdAsync(id.ToString()))?.UserName;
        }
        public async Task<bool> IsInRole(int id, Roles role)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return await _userManager.IsInRoleAsync(user!, role.ToString());
        }

        public async Task ChangeUserPermissionsAsync(int userId, List<string> strPermissions)
        {
            var user = await _context.Users.Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException(nameof(ApplicationUser), userId);

            var permissions = await _context.Permissions.Where(p => strPermissions.Contains(p.Name)).ToListAsync();

            user.Permissions = permissions;
            await _context.SaveChangesAsync();
        }
    }
}
