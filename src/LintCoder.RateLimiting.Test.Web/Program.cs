using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddQoS(builder.Configuration, configure: (qosBuilder) =>
{
    qosBuilder.OnLimitProcessResult += (requestIdentity, quotaConfig, isAllow, waittimeMills) =>
    {
        builder.Services.BuildServiceProvider().GetRequiredService<ILogger<Program>>().LogDebug("requestIdentity->{0},isAllow->{1},waittimeMills->{2}", requestIdentity, isAllow, waittimeMills);
    };
    qosBuilder.OnFallbackAction += async (exception, keyValuePairs, cancellationToken) =>
    {
        var requestDelegate = keyValuePairs["RequestDelegate"] as RequestDelegate;
        var httpContext = keyValuePairs["HttpContext"] as HttpContext;
        httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
        await httpContext.Response.WriteAsync("Request too many");
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseQoS();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
