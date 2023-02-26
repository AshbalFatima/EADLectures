using Apis.Common;
using Apis.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using System.Net.Http;
using VS_APis.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//#if DEBUG

builder.Services.AddMongo();
//#else

//builder.Services.AddProductionDB();

//#endif



builder.Services.AddRepository<Customer>("customers");



builder.Services.AddHttpClient<ProductClient>(client => {
    client.BaseAddress = new Uri("https://localhost:5001");
}).AddTransientHttpErrorPolicy(b=> b.WaitAndRetryAsync(5,
retryAttempt =>TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)),
onRetry: (outcome, timespane, retryAttempt) =>
{
    var logger = builder.Services.BuildServiceProvider();
    logger.GetService<ILogger<ProductClient>>()?.LogWarning($"Delaying {timespane.TotalSeconds} seconds, then making retry {retryAttempt}");
}
))
.AddTransientHttpErrorPolicy(bl=>bl.Or<TimeoutRejectedException>().CircuitBreakerAsync(
    3,
    TimeSpan.FromSeconds(15),
    onBreak: (outcome, timespane) => {
        var logger = builder.Services.BuildServiceProvider();
        logger.GetService<ILogger<ProductClient>>()?.LogWarning($"Opening Circuit ");

    }
    ))
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
