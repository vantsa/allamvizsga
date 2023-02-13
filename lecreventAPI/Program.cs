global using lecreventAPI.Models;
global using lecreventAPI.Services.UserService;
global using lecreventAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddCors(p => p.AddPolicy("corspolicy",build=> 
{
    build.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
}));

//builder.Services.AddDbContext<DataContext>(options => options.UseMySql(Configuration.GetConnectionString("Default")));

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("Default");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(connectionString,serverVersion));

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

