global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using dotnet_rpg.Services.CharacterService;
global using dotnet_rpg.Models;
global using dotnet_rpg.Dtos.Character;
global using dotnet_rpg.Data;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(); // Carrega as variáveis de ambiente do arquivo .env

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => 
    options.UseNpgsql($"Host={Environment.GetEnvironmentVariable("HOST")};" +
                    $"Port={Environment.GetEnvironmentVariable("PORT")};" +
                    $"Database={Environment.GetEnvironmentVariable("DATABASE")};" +
                    $"Username={Environment.GetEnvironmentVariable("USERNAME")};" +
                    $"Password={Environment.GetEnvironmentVariable("PASSWORD")};" +
                    "TrustServerCertificate=true")
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
