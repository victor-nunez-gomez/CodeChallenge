using CodeChallenge.Fees.Interfaces;
using CodeChallenge.Fees.Services;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
var _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

_config.AddJsonFile($"appsettings.{_env}.json", optional: true);

// Add services to the container.
builder.Services.AddSingleton<IFeeService, FeeService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
