using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcWine.Data;
using DotNetEnv;
using NyWine.Wines;
using NyWine.RabbitMQ;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Configure the database connection for MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?
    .Replace("MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER"))
    .Replace("MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"));

builder.Services.AddDbContext<MvcWineContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var rabbitMQConfig = new RabbitMQConfiguration();
builder.Configuration.GetSection("RabbitMQ").Bind(rabbitMQConfig);
builder.Services.AddSingleton(rabbitMQConfig);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<RabbitMQProducer>();

builder.Services.AddScoped<WineQueries>();
builder.Services.AddScoped<WineCommands>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
