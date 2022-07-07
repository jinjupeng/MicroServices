using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace LintCoder.Shared.Authorization
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册授权组件
        /// PermissionHandlerLocal  本地授权
        /// </summary>
        /// <typeparam name="TAuthorizationHandler"></typeparam>
        public static IServiceCollection AddAuthorization<TAuthorizationHandler>(this IServiceCollection services)
            where TAuthorizationHandler : AbstractPermissionHandler
        {
            services
                .AddScoped<IAuthorizationHandler, TAuthorizationHandler>();
            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy(AuthorizePolicy.Default, policy =>
                    {
                        policy.Requirements.Add(new PermissionRequirement());
                    });
                });

            return services;
        }
    }
}
