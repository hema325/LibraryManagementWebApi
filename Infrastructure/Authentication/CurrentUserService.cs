using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    internal class CurrentUserService: ICurrentUser
    {
        private readonly HttpContext _context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context.HttpContext!;
        }

        public int? Id
        {
            get
            {
                var id = _context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return id == null ? null : int.Parse(id);
            }
        }

        public string? UserName => _context.User.FindFirstValue(ClaimTypes.Name);
    }
}
