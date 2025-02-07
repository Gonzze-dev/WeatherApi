using RedisDB;
using StackExchange.Redis;
using WeatherApi.Helpers;
using WeatherApi.Interfaces;
using WeatherApi.Repository;
using WeatherApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//HTTP
builder.Services.AddSingleton<HttpClient>();

//DB
builder.Services.AddSingleton<RedisDBConnection>(); 

//Repository
builder.Services.AddKeyedScoped<IRepositoryWeatherForecast<HttpResponseMessage?>,ApiRepository>("ApiRepository");
builder.Services.AddKeyedScoped<IRepositoryWeatherForecast<RedisValue>, RedisCacheRepository>("RedisCacheRepository");
builder.Services.AddScoped<RedisCacheRepository>();

//Service
builder.Services.AddScoped<WeatherForecastService>();

//HelperService
builder.Services.AddScoped<WeatherForecastRepositoryHelper>();

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
