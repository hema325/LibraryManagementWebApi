using Application.Authentication.Common;
using Infrastructure.Authentication.Constants;
using Infrastructure.Authentication.Settings;
using Infrastructure.Identity.Models;
using Infrastructure.Multitenancy.Constants;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Authentication
{
    internal class AuthenticationService : IAuthentication
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenSettings _jwtTokenSettings;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        private readonly IDateTime _dateTime;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentTenant _currentTenant;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
                                     IOptions<JwtTokenSettings> jwtTokenSettings,
                                     IOptions<RefreshTokenSettings> refreshTokenSettings,
                                     IDateTime dateTime,
                                     ApplicationDbContext context,
                                     ICurrentTenant currentTenant)
        {
            _userManager = userManager;
            _jwtTokenSettings = jwtTokenSettings.Value;
            _refreshTokenSettings = refreshTokenSettings.Value;
            _dateTime = dateTime;
            _context = context;
            _currentTenant = currentTenant;
        }

        public async Task<AuthResult> RegisterAsync(string userName, string password, List<string> roles, List<string> strPermissions)
        {
            var permissions = await _context.Permissions.Where(p => strPermissions.Contains(p.Name)).ToListAsync();

            var user = new ApplicationUser
            {
                UserName = userName,
                Permissions = permissions
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new ValidationException(result.Errors.Select(e => e.Description).ToArray());

            result = await _userManager.AddToRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                throw new ValidationException(result.Errors.Select(e => e.Description).ToArray());
            }

            var jwtToken = GenerateJwtSecurityToken(await GetUserClaimsAsync(user));
            var refreshToken = await GetFirstActiveOrCreateNewRefreshTokenAsync(user);

            return new AuthResult()
            {
                UserId = user.Id,
                UserName = user.UserName,
                JwtToken = WriteToken(jwtToken),
                JwtTokenExpiresOn = jwtToken.ValidTo,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresOn = refreshToken.ExpriesOn
            };
        }
        
        public async Task<AuthResult> AuthenticateAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                throw new AuthenticationException();

            var jwtToken = GenerateJwtSecurityToken(await GetUserClaimsAsync(user));
            var refreshToken = await GetFirstActiveOrCreateNewRefreshTokenAsync(user);

            return new AuthResult
            {
                UserId = user.Id,
                UserName = user.UserName!,
                JwtToken = WriteToken(jwtToken),
                JwtTokenExpiresOn = jwtToken.ValidTo,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresOn = refreshToken.ExpriesOn
            };
        }

        public async Task<AuthResult> RequestJwtTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken && _dateTime.Now < r.ExpriesOn && r.RevokedOn == null));

            if (user == null)
                throw new InvalidTokenException();

            var oldRefreshToken = user.RefreshTokens.First(r => r.Token == refreshToken && _dateTime.Now < r.ExpriesOn && r.RevokedOn == null);
            oldRefreshToken.RevokedOn = _dateTime.Now;

            await _userManager.UpdateAsync(user);

            var jwtToken = GenerateJwtSecurityToken(await GetUserClaimsAsync(user));
            var newRefreshToken = await GetFirstActiveOrCreateNewRefreshTokenAsync(user);

            return new AuthResult
            {
                UserId = user.Id,
                UserName = user.UserName!,
                JwtToken = WriteToken(jwtToken),
                JwtTokenExpiresOn = jwtToken.ValidTo,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiresOn = newRefreshToken.ExpriesOn
            };
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken && _dateTime.Now < r.ExpriesOn && r.RevokedOn == null));

            if (user == null)
                throw new InvalidTokenException();

            var oldRefreshToken = user.RefreshTokens.First(r => r.Token == refreshToken && _dateTime.Now < r.ExpriesOn && r.RevokedOn == null);
            oldRefreshToken.RevokedOn = _dateTime.Now;

            await _userManager.UpdateAsync(user);
        }

        private JwtSecurityToken GenerateJwtSecurityToken(IEnumerable<Claim> claims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer:_jwtTokenSettings.Issuer,
                audience:_jwtTokenSettings.Audience,
                claims: claims,
                expires: _dateTime.Now.AddMinutes(_jwtTokenSettings.ExpirationInMinutes),
                signingCredentials: signingCredentials);
        }

        private async Task<RefreshToken> GetFirstActiveOrCreateNewRefreshTokenAsync(ApplicationUser user)
        {
            var refreshToken = user.RefreshTokens?.FirstOrDefault(t => _dateTime.Now < t.ExpriesOn && t.RevokedOn == null);
            
            if (refreshToken == null)
            {
                refreshToken = GenerateRefreshToken();

                if (user.RefreshTokens == null)
                    user.RefreshTokens = new List<RefreshToken>();

                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            return refreshToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var bytes = new byte[_refreshTokenSettings.Length];
            RandomNumberGenerator.Create().GetBytes(bytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(bytes),
                CreatedOn = _dateTime.Now,
                ExpriesOn = _dateTime.Now.AddDays(_refreshTokenSettings.ExpirationInDays)
            };
        }

        private async Task<IEnumerable<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(TenancyHeaders.Tenant, _currentTenant.Id ?? string.Empty)
            }
            .Union((await _userManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r)))
            .Union((await _context.Permissions.Where(p => p.Users.Any(u => u.Id == user.Id)).ToListAsync()).Select(p => new Claim(CustomClaims.Permission, p.Name)));
        }

        private string WriteToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
