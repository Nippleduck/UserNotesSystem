using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserNotesSystem.Data.Context;
using UserNotesSystem.Data.Identity;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace UserNotesSystem.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotesContext>(builder =>
                builder.UseNpgsql(configuration.GetConnectionString("Default"), 
                options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

            services.AddIdentityCore<ApplicationUser>()
                .AddSignInManager()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<NotesContext>();

            services.AddScoped<ContextInitialiser>();
            services.AddTransient<IdentityService>();
            
            return services;
        }
    }
}
