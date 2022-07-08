using Lintcoder.Base;
using LintCoder.Identity.API.Application.Behaviors;
using LintCoder.Identity.API.Application.Models.Enum;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.API.Infrastructure.Authorization;
using LintCoder.Identity.API.Infrastructure.Services;
using LintCoder.Identity.API.Middlewares;
using LintCoder.Identity.Infrastructure;
using LintCoder.Identity.Infrastructure.Repositories;
using LintCoder.Shared.Authentication;
using LintCoder.Shared.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseStatusCodePages(context => {
    var response = context.HttpContext.Response;
    if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        var payload = JsonSerializer.Serialize(MsgModel.Fail(ResponseTypeEnum.Unauthorized, "很抱歉，您无权访问该接口!"));
        response.ContentType = "application/json";
        response.StatusCode = StatusCodes.Status200OK;
        response.WriteAsync(payload);
    }

    return Task.CompletedTask;
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


