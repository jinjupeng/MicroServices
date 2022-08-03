using LintCoder.Infrastructure.Authentation.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LintCoder.Infrastructure.Auth.JwtBearer
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtOptions(this IServiceCollection services, Action<JwtSettings> jwtSettings)
        {
            if (jwtSettings == null)
            {
                throw new ArgumentNullException(nameof(jwtSettings));
            }
            services.Configure(jwtSettings);

            return services;
        }

        public static IServiceCollection AddJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettings);

            return services;
        }


        public static IServiceCollection AddLCAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtOptions(configuration);
            var jwtSettings = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;

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
                    options.TokenValidationParameters = JwtHelpers.GenarateTokenValidationParameters(jwtSettings);
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
                            var user = context.Principal;
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}