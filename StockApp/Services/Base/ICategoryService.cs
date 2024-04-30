
using StockApp.Models;

namespace StockApp.Services.Base;

public interface ICategoryService
{
    public Task<IEnumerable<Category>> GetAllCategoriesAsync();
    public Task<Category> GetCategoryByIdAsync(int id);
    public Task AddCategoryAsync(Category category);
    public Task UpdateCategoryAsync(Category category);
    public Task DeleteCategoryAsync(int id);

}
