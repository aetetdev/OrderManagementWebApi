using Microsoft.AspNetCore.Mvc;
using OrderManagementWebApi.Dtos;
using OrderManagementWebApi.Services;

namespace OrderManagementWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            var (success, error, order) = await _service.CreateOrderAsync(dto);
            if (!success) return BadRequest(new { error });
            var result = new OrderDetailDto
            {
                Id = order!.Id,
                UserId = order.UserId,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderDetailItemDto { ProductId = i.ProductId, ProductName = i.ProductName, Quantity = i.Quantity, UnitPrice = i.UnitPrice }).ToList(),
                Total = order.Total
            };
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var list = (await _service.GetByUserAsync(userId)).Select(o => new OrderListDto { Id = o.Id, CreatedAt = o.CreatedAt, Total = o.Total }).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var o = await _service.GetByIdAsync(id);
            if (o == null) return NotFound();
            var dto = new OrderDetailDto { Id = o.Id, UserId = o.UserId, CreatedAt = o.CreatedAt, Items = o.Items.Select(i => new OrderDetailItemDto { ProductId = i.ProductId, ProductName = i.ProductName, Quantity = i.Quantity, UnitPrice = i.UnitPrice }).ToList(), Total = o.Total };
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
