using Microsoft.AspNetCore.Mvc;
using OrderManagementWebApi.Dtos;
using OrderManagementWebApi.Models;
using OrderManagementWebApi.Repositories;

namespace OrderManagementWebApi.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class ProductsController : ControllerBase
     {
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo) => _repo = repo;

         [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto dto)
        {
            var p = new Product { Name = dto.Name, Stock = dto.Stock, Price = dto.Price };
            await _repo.AddAsync(p);
            return CreatedAtAction(nameof(GetById), new { id = p.Id }, p);
        }
     }
}
