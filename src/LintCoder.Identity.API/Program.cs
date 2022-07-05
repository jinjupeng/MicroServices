using Lintcoder.Base;
using LintCoder.Identity.API.Application.Behaviors;
using LintCoder.Identity.API.Middlewares;
using LintCoder.Identity.Infrastructure;
using LintCoder.Identity.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContextPool<IdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), optionsSqlServer => {
        optionsSqlServer.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
    })); 
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

//builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


