global using lecreventAPI.Models;
global using lecreventAPI.Services.UserService;
global using lecreventAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("Default");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));
builder.Services.AddDbContext<DataContext>(options => options.UseMySql(connectionString,serverVersion));



// builder.Services.AddDistributedMemoryCache();
// builder.Services.AddSession(options =>
// {
//     options.Cookie.Name = ".Lecrevent.Session";
//     options.Cookie.IsEssential = true;
//     options.Cookie.HttpOnly = true;
//     options.IdleTimeout = TimeSpan.FromMinutes(30);
// });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
options.TokenValidationParameters = new TokenValidationParameters
{
ValidateIssuerSigningKey = true,
IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("lecrevent_vantsa_allamvizsga2k23")),
ValidateIssuer = false,
ValidateAudience = false
};
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();
Console.WriteLine("valami");

app.MapControllers();

app.UseAuthentication();

app.Run();

