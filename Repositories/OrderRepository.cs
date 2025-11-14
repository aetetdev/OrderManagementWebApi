using Microsoft.EntityFrameworkCore;
using OrderManagementWebApi.Data;
using OrderManagementWebApi.Models;

namespace OrderManagementWebApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;
        public async Task AddAsync(Order order)
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
        }
        public async Task<Order?> GetByIdAsync(int id) => await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId) => await _db.Orders.Include(o => o.Items).Where(o => o.UserId == userId).ToListAsync();
        public async Task DeleteAsync(int id)
        {
            var o = await _db.Orders.FindAsync(id);
            if (o!=null) { _db.Orders.Remove(o); await _db.SaveChangesAsync(); }
        }
    }
}
