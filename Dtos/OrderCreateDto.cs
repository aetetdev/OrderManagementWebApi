using System.Collections.Generic;

namespace OrderManagementWebApi.Dtos
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public List<OrderCreateItemDto> Items { get; set; } = new();
    }

    public class OrderCreateItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
