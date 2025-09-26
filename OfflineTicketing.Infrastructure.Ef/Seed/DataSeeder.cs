using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfflineTicketing.Core.Entities;
using OfflineTicketing.Core.Enums;
using OfflineTicketing.Infrastructure.Ef.Context;

namespace OfflineTicketing.Infrastructure.Ef.Seed
{


    public class DataSeeder(IServiceProvider sp) : IHostedService
    {
        public async Task StartAsync(CancellationToken ct)
        {
            using var scope = sp.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<TicketingContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();

            await ctx.Database.MigrateAsync(ct);

            if (!await ctx.Users.AnyAsync(u => u.Role == RoleTypeEnum.Admin, ct))
            {
                var admin = User.Create("System Admin", "admin@local", "Admin@1234567890#Admin", RoleTypeEnum.Admin);
                var employee = User.Create("System Employee", "employee@local", "Employee@1234567890#Employee", RoleTypeEnum.Employee);
                admin.UpdatePassword(hasher.HashPassword(admin, "Admin@1234567890#Admin"));
                employee.UpdatePassword(hasher.HashPassword(admin, "Employee@1234567890#Employee"));
                ctx.Users.Add(admin);
                ctx.Users.Add(employee);
                await ctx.SaveChangesAsync(ct);
            }
        }

        public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
    }

}
