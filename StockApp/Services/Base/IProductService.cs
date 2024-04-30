
using StockApp.Models;

namespace StockApp.Services.Base;

public interface IProductService
{
    public IEnumerable<Product> GetAllProducts();
    public Task AddProductAsync(Product product);
    public Task UpdateProductAsync(Product product);
    public Task DeleteProductAsync(int id);
    public Task<IEnumerable<Product>> SearchProductAsync(string searchProduct);
    public  Task<IEnumerable<Product>> FilterProductsByCategoriesAsync(List<int> categoryIds);
}
