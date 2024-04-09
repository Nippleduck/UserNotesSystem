using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UserNotesSystem.Authentication.Models;
using UserNotesSystem.Authentication.Constants;
using UserNotesSystem.Core.Constants;
using System.Text;

namespace UserNotesSystem.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<TokenFactory>();

            var clientSecrets = configuration.GetSection(nameof(ClientSecrets));
            services.Configure<ClientSecrets>(options => options.SecretKey = clientSecrets[nameof(ClientSecrets.SecretKey)]!);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(clientSecrets[nameof(ClientSecrets.SecretKey)]!));

            services.Configure<TokenOptions>(options =>
            {
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.TokenValidationParameters = tokenValidationParameters;
                config.SaveToken = true;

            });

            services.AddAuthorizationBuilder()
                .AddPolicy(Policies.AdminOnlyAccess, policy => policy.RequireRole(Roles.Administrator.ToString()));

            return services;
        } 
    }
}
