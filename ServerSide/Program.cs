using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using ServerSide.BusinessLogic;
using ServerSide.BusinessLogic.Interfaces;
using ServerSide.PalindromeValidator;
using ServerSide.PalindromeValidator.Interfaces;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

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
        options.QueueLimit = 0; // в очереди никто не может ждать, сразу отправляем 503
    }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Palindrome API",
        Description = "An ASP.NET Core Web API для проверки палиндромов"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}
    );
builder.Services.AddScoped<IPalindromeService, PalindromeService>();
builder.Services.AddScoped<IPalindromeValidator, PalindromeValidator>();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.Response;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
app.UseRateLimiter();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
