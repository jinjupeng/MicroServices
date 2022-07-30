using LintCoder.Shared.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LintCoder.Shared.Authentication
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtOptions(this IServiceCollection services, Action<JwtOptions> jwtOptions)
        {
            if (jwtOptions == null)
            {
                throw new ArgumentNullException(nameof(jwtOptions));
            }
            services.Configure(jwtOptions);
            services.AddSingleton<JwtHelper>();

            return services;
        }

        public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptionsConfig = configuration.GetSection(nameof(JwtOptions));
            services.Configure<JwtOptions>(jwtOptionsConfig);
            services.AddSingleton<JwtHelper>();

            return services;
        }

        public static IServiceCollection AddUserContext(this IServiceCollection services)
        {
            services.AddScoped<UserContext>();

            return services;
        }


        public static IServiceCollection AddBasicAuthentication(this IServiceCollection services)
        {
            var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;
            var jwtHelper = services.BuildServiceProvider().GetService<JwtHelper>();

            services.AddAuthentication(s =>
            {
                //添加JWT Scheme
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // 添加JwtBearer验证服务：
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = jwtHelper.GenarateTokenValidationParameters();
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            // 如果token过期，则把<是否过期>添加到返回头信息中
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = (context) =>
                        {
                            var userContext = context.HttpContext.RequestServices.GetService<UserContext>();
                            var claims = context.Principal.Claims;
                            userContext.Id = long.Parse(claims.First(x => x.Type == JwtClaims.UserId).Value);
                            userContext.Account = claims.First(x => x.Type == JwtClaims.UserName).Value;
                            userContext.Name = claims.First(x => x.Type == JwtClaims.NickName).Value;
                            userContext.RoleIds = claims.First(x => x.Type == JwtClaims.RoleIds).Value;
                            userContext.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}