namespace OrderManagementWebApi.Dtos
{
 public class ProductCreateDto
 {
 public string Name { get; set; } = null!;
 public int Stock { get; set; }
 public decimal Price { get; set; }
 }
}
