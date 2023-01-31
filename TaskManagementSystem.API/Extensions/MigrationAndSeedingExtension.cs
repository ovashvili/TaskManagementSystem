using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;
using TaskManagementSystem.Application.Contracts;
using AutoMapper;
using TaskManagementSystem.Infrastructure.Models.Entities;

namespace TaskManagementSystem.Infrastructure.Extensions;

public static class MigrationAndSeedingExtension
{
    public async static Task AutomateMigrationAndSeeding(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<AppDBContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userService = services.GetRequiredService<IUserService>();
                var mapper = services.GetRequiredService<IMapper>();
                context.Database.Migrate();
                await ContextSeed.SeedRolesAsync(userManager, roleManager);
                await ContextSeed.SeedSudoAsync(userService, userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}
