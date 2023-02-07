using CodeChallenge.Api.Security;
using CodeChallenge.Cards.Interfaces;
using CodeChallenge.Cards.Services;
using CodeChallenge.DataAccess.Context;
using CodeChallenge.DataAccess.Interfaces;
using CodeChallenge.DataAccess.Repositories;
using CodeChallenge.Fees.Interfaces;
using CodeChallenge.Fees.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
var _env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

_config.AddJsonFile($"appsettings.{_env}.json", optional: true);

// Add services to the container.
builder.Services.AddSingleton<IFeeService, FeeService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add authentication
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("LocalDB"));

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
