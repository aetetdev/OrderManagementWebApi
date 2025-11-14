using Microsoft.EntityFrameworkCore;
using OrderManagementWebApi.Data;
using OrderManagementWebApi.Repositories;
using OrderManagementWebApi.Services;
using OrderManagementWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// register repositories and service
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 app.UseSwagger();
 app.UseSwaggerUI();

 // Ensure DB created and seed in development
 using var scope = app.Services.CreateScope();
 var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
 db.Database.EnsureCreated();

 if (!db.Products.Any())
 {
 db.Products.AddRange(
 new Product { Id =1, Name = "Product A", Stock =10, Price =9.99m },
 new Product { Id =2, Name = "Product B", Stock =5, Price =19.50m }
 );
 db.SaveChanges();
 }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
