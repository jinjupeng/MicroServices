using LintCoder.Shared.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,// 是否验证Issuer
                        ValidateAudience = true,// 是否验证Audience
                        ValidateLifetime = true,// 是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,// 是否验证SecurityKey
                        ValidAudience = jwtOptions.Audience,// Audience
                        ValidIssuer = jwtOptions.Issuer,// Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))// 拿到SecurityKey
                    };
                    options.Events.OnTokenValidated = (context) =>
                    {
                        var userContext = context.HttpContext.RequestServices.GetService<UserContext>();
                        var claims = context.Principal.Claims;
                        userContext.Id = long.Parse(claims.First(x => x.Type == JwtRegisteredClaimNames.NameId).Value);
                        userContext.Account = claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
                        userContext.Name = claims.First(x => x.Type == JwtRegisteredClaimNames.Name).Value;
                        userContext.RoleIds = claims.First(x => x.Type == "roleids").Value;
                        userContext.RemoteIpAddress = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                        return Task.CompletedTask;
                    };
                });

            return services;
        }
    }
}