using RedisDB;
using WeatherApi.Interfaces;
using WeatherApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//HTTP
builder.Services.AddSingleton<HttpClient>();

//DB
builder.Services.AddSingleton<RedisDBConnection>();

//Repository
builder.Services.AddKeyedScoped<IRepositoryWeatherForecast,ApiRepository>("ApiRepository");
builder.Services.AddKeyedScoped<IRepositoryWeatherForecast, RedisCacheRepository>("RedisCacheRepository");

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
