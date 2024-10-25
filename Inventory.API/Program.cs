using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration  = builder.Configuration;

var connectionString = configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
Console.WriteLine($"Test Connection {connectionString} ");

builder.Services.AddControllers();

// Database configuration
//builder.Services.AddDbContext<CustomerDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Inventory.API")));


// Register services
builder.Services.AddScoped<ICustomerService, CustomerService>();
//builder.Services.AddScoped<ICustomerRepository, ICustomerRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
