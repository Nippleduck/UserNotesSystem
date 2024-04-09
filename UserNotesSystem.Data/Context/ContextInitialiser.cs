using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UserNotesSystem.Core.Constants;
using UserNotesSystem.Data.Identity;

namespace UserNotesSystem.Data.Context
{
    public static class InitialiserExtension
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ContextInitialiser>();

            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }
    }

    public class ContextInitialiser(
        ILogger<ContextInitialiser> logger,
        NotesContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
        )
    {
        public async Task InitialiseAsync()
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            var administratorRole = new IdentityRole(Roles.Administrator.ToString());
            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var regularRole = new IdentityRole(Roles.Regular.ToString());
            if (roleManager.Roles.All(r => r.Name != regularRole.Name))
            {
                await roleManager.CreateAsync(regularRole);
            }

            var administrator = new ApplicationUser { UserName = "admin" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "admin");
                if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                {
                    await userManager.AddToRolesAsync(administrator, [ administratorRole.Name ]);
                }
            }
        }
    }
}
