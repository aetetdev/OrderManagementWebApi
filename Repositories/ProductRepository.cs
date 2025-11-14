using Microsoft.EntityFrameworkCore;
using OrderManagementWebApi.Data;
using OrderManagementWebApi.Models;

namespace OrderManagementWebApi.Repositories
{
 public class ProductRepository : IProductRepository
 {
 private readonly AppDbContext _db;
 public ProductRepository(AppDbContext db) => _db = db;
 public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FindAsync(id);
 public async Task<IEnumerable<Product>> GetAllAsync() => await _db.Products.ToListAsync();
 public async Task AddAsync(Product product)
 {
 await _db.Products.AddAsync(product);
 await _db.SaveChangesAsync();
 }
 public async Task UpdateAsync(Product product)
 {
 _db.Products.Update(product);
 await _db.SaveChangesAsync();
 }
 }
}
