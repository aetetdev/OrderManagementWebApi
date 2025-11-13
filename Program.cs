using Microsoft.EntityFrameworkCore;
using OrderManagementWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// service & repo kayýtlarýný buraya ekleyin
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
// builder.Services.AddScoped<IOrderRepository, OrderRepository>();
// builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
 app.UseSwagger();
 app.UseSwaggerUI();

 // Auto-migrate in development
 using var scope = app.Services.CreateScope();
 var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
 db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
