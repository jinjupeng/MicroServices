using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using SkyApm.Utilities.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddOcelot(builder.Configuration);
builder.Configuration.AddJsonFile("ocelot.global.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("ocelot.identity.api.json", optional: false, reloadOnChange: true);
//builder.Services.AddSwaggerForOcelot(builder.Configuration);
//builder.Services.AddOcelot(builder.Configuration).AddConsul().AddConfigStoredInConsul();
builder.Services.AddSkyApmExtensions(); //Ìí¼ÓSkywalkingÏà¹ØÅäÖÃ


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerForOcelotUI(opt => {
    //    opt.PathToSwaggerGenerator = "/swagger/docs";
    //});
}

app.UseHttpsRedirection();

//await app.UseOcelot();

app.UseAuthorization();

app.MapControllers();

app.Run();
