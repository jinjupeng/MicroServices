using CSRedis;
using FluentValidation;
using Lintcoder.Base;
using LintCoder.Consul;
using LintCoder.Identity.API.Application.Behaviors;
using LintCoder.Identity.API.Application.Models.Enum;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.API.HealthChecks;
using LintCoder.Identity.API.Infrastructure.Authorization;
using LintCoder.Identity.API.Infrastructure.Services;
using LintCoder.Identity.API.Middlewares;
using LintCoder.Identity.Infrastructure;
using LintCoder.Identity.Infrastructure.Repositories;
using LintCoder.Shared.Authentication;
using LintCoder.Shared.Authorization;
using LintCoder.Shared.MongoDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Net;
using System.Reflection;
using System.Text.Json;

// Loads appsetting.json and enables ${configsetting}
var logger = LogManager.Setup()
                       .LoadConfigurationFromAppSettings()
                       .GetCurrentClassLogger();
IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile(path: "appsettings.json").Build();
ConfigSettingLayoutRenderer.DefaultConfiguration = config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => {
    // 以驼峰命名方式序列化字段
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), optionsSqlServer => {
        optionsSqlServer.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
    })); 
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddBasicAuthentication();
builder.Services.AddAuthorization<PermissionLocalHandler>();
builder.Services.AddUserContext();

//builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddTransient<ISysApiService, SysApiService>();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .SetIsOriginAllowed(origin => true)
                          .SetPreflightMaxAge(new TimeSpan(0, 10, 0))
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); //指定处理cookie
                      });
});

builder.Services.AddHealthChecks(builder.Configuration);
builder.Services.AddConsul(builder.Configuration);

RedisHelper.Initialization(new CSRedisClient(builder.Configuration.GetConnectionString("CSRedisConnection")));

builder.Services.AddMongoOptions(builder.Configuration);
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseConsul(app.Lifetime);
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseStatusCodePages(context => {
    var response = context.HttpContext.Response;
    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        var jsonSeralizeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var payload = JsonSerializer.Serialize(MsgModel.Fail(ResponseTypeEnum.Unauthorized, "很抱歉，您无权访问该接口!"), jsonSeralizeOptions);
        response.ContentType = "application/json";
        response.StatusCode = StatusCodes.Status200OK;
        response.WriteAsync(payload);
    }

    return Task.CompletedTask;
});

app.UseAuthentication();

app.UseAuthorization();

app.UseHealthChecks();

app.MapControllers();

app.Run();


