using OrderManagementWebApi.Dtos;
using OrderManagementWebApi.Models;
using OrderManagementWebApi.Repositories;

namespace OrderManagementWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;
        public OrderService(IProductRepository productRepo, IOrderRepository orderRepo)
        {
            _productRepo = productRepo;
            _orderRepo = orderRepo;
        }
        public async Task<(bool Success, string? Error, Order? Order)> CreateOrderAsync(OrderCreateDto dto)
        {
            // validate products and stock
            var items = new List<OrderItem>();
            foreach (var it in dto.Items)
            {
                var prod = await _productRepo.GetByIdAsync(it.ProductId);
                if (prod == null) 
                { 
                    return (false, $"{it.ProductId} Numaralı ürün bulunamadı", null); 
                }
                if (prod.Stock < it.Quantity) 
                { 
                    return (false, $"{prod.Name} isimli üründen {prod.Stock} adet var {it.Quantity} sipariş verilemez", null); 
                }
                items.Add(new OrderItem { ProductId = prod.Id, ProductName = prod.Name, Quantity = it.Quantity, UnitPrice = prod.Price });
            }
            // deduct stock and save
            var order = new Order { UserId = dto.UserId, CreatedAt = DateTime.UtcNow, Items = items };
            // update product stocks
            foreach (var it in items)
            {
                var p = await _productRepo.GetByIdAsync(it.ProductId)!;
                p.Stock -= it.Quantity;
                await _productRepo.UpdateAsync(p);
            }
            await _orderRepo.AddAsync(order);
            return (true, null, order);
        }
        public async Task<IEnumerable<Order>> GetByUserAsync(int userId) => await _orderRepo.GetByUserIdAsync(userId);
        public async Task<Order?> GetByIdAsync(int id) => await _orderRepo.GetByIdAsync(id);
        public async Task DeleteAsync(int id) => await _orderRepo.DeleteAsync(id);
    }
}
