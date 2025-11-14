using OrderManagementWebApi.Models;

namespace OrderManagementWebApi.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task DeleteAsync(int id);
    }
}
