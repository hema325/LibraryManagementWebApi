using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    internal class ApplicationUser: IdentityUser<int>
    {
        public List<RefreshToken> RefreshTokens { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
