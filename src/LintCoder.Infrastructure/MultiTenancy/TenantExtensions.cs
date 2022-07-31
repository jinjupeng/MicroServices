using LintCoder.Application.Multitenancy;
using LintCoder.Shared.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Infrastructure.MultiTenancy
{
    public static class TenantServiceExtensions
    {
        public static IServiceCollection AddMultitenancy(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddDbContext<TenantDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                )
                .AddMultiTenant<TenantEntity>()
                    .WithClaimStrategy(JwtClaims.Tenant)
                    .WithHeaderStrategy(MultitenancyConstants.TenantIdName)
                    .WithQueryStringStrategy(MultitenancyConstants.TenantIdName)
                    .WithEFCoreStore<TenantDbContext, TenantEntity>()
                    .Services
                .AddScoped<ITenantService, TenantService>();
        }

        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app) =>
            app.UseMultiTenant();

        private static FinbuckleMultiTenantBuilder<TenantEntity> WithQueryStringStrategy(this FinbuckleMultiTenantBuilder<TenantEntity> builder, string queryStringKey) =>
            builder.WithDelegateStrategy(context =>
            {
                if (context is not HttpContext httpContext)
                {
                    return Task.FromResult((string?)null);
                }

                httpContext.Request.Query.TryGetValue(queryStringKey, out StringValues tenantIdParam);

                return Task.FromResult((string?)tenantIdParam.ToString());
            });
    }
}
