using Infrastructure.Authentication.Settings;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundServices
{
    internal class RemoveExpiredRefreshTokensBackgroundService : BackgroundService
    {
        private readonly IDateTime _dateTime;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        private readonly IServiceProvider _serviceProvider;

        public RemoveExpiredRefreshTokensBackgroundService(IServiceProvider serviceProvider, IDateTime dateTime, IOptions<RefreshTokenSettings> refreshTokenSettings)
        {
            _refreshTokenSettings = refreshTokenSettings.Value;
            _serviceProvider = serviceProvider;
            _dateTime = dateTime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromDays(_refreshTokenSettings.RemoveExpiredInDays));
            while (await periodicTimer.WaitForNextTickAsync() && !stoppingToken.IsCancellationRequested) 
            {
                var userManager = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var users = await userManager.Users.Where(u => u.RefreshTokens.Any(rt => _dateTime.Now >= rt.ExpriesOn || rt.RevokedOn != null)).ToListAsync();

                users.ForEach(u => u.RefreshTokens.RemoveAll(rt => _dateTime.Now >= rt.ExpriesOn || rt.RevokedOn != null));
                await Task.WhenAll(users.Select(u => userManager.UpdateAsync(u)));
            }
        }
    }

}
