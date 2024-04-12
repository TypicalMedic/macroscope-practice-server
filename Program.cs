using Microsoft.AspNetCore.RateLimiting;
using ServerSide.BusinessLogic;
using ServerSide.BusinessLogic.Interfaces;
using ServerSide.Controllers.Interfaces;
using ServerSide.PalindromeValidator;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
int maxConnections;
if (!int.TryParse(configuration["requestlimit"], out maxConnections) || maxConnections <= 0)
{
    Console.WriteLine("Error: Command line argument 'requestlimit' should be positive int");
    Environment.Exit(1);
}

// Add services to the container.
var concurrencyPolicy = "Concurrency";
builder.Services.AddRateLimiter(_ => _
    .AddConcurrencyLimiter(policyName: concurrencyPolicy, options =>
    {
        options.PermitLimit = maxConnections; // максимальное количество текущих подключений
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0; // в очереди никто не может ждать, сразу отправляем 502
    }));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPalindromeService, PalindromeService>();
builder.Services.AddScoped<IPalindromeValidator, PalindromeValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
