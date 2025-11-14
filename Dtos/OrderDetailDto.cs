using System;
using System.Collections.Generic;

namespace OrderManagementWebApi.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailItemDto> Items { get; set; } = new();
        public decimal Total { get; set; }
    }
    public class OrderDetailItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
