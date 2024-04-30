
namespace StockApp.Models;

public class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
    public int? Count { get; set; }

    public List<ProductCategory> ProductCategories { get; set; }

}
