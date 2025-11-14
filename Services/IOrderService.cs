using OrderManagementWebApi.Models;
using OrderManagementWebApi.Dtos;

namespace OrderManagementWebApi.Services
{
    public interface IOrderService
    {
        Task<(bool Success, string? Error, Order? Order)> CreateOrderAsync(OrderCreateDto dto);
        Task<IEnumerable<Order>> GetByUserAsync(int userId);
        Task<Order?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
