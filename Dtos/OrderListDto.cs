using System;

namespace OrderManagementWebApi.Dtos
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
    }
}
