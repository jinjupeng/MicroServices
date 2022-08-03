using LintCoder.Application.Common.Interfaces;
using LintCoder.Infrastructure.Auth.JwtBearer;
using LintCoder.Infrastructure.Authorization;
using LintCoder.Infrastructure.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCurrentUser();
            services.AddLCAuthorization();
            services.AddLCAuthentication(configuration);
            return services;
        }

        private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

        private static IServiceCollection AddLCAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, LCRoleAuthorizeHandler>();
            services.AddAuthorization(config =>
            {
                config.AddPolicy("LintCoderRole", policy =>
                {
                    policy.Requirements.Add(new LCRoleRequirement(LCRoleConstants.Admin, LCRoleConstants.Basic));
                });
            });

            return services;
        }
    }
}
