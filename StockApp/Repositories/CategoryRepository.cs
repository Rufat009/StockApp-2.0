
using Microsoft.EntityFrameworkCore;
using StockApp.Data;
using StockApp.Models;
using StockApp.Repositories.Base;

namespace StockApp.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly StockAppDbContext dbContext;

    public CategoryRepository(StockAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var allCategories = await dbContext.Categories.ToListAsync();

        return allCategories;
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var searchedCategory = await dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);

        return searchedCategory!;
    }

    public async Task AddAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        dbContext.Categories.Update(category);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var deleteCategory = await dbContext.Categories.FirstOrDefaultAsync(category => category.Id == id);

        dbContext.Categories.Remove(deleteCategory!);

        await dbContext.SaveChangesAsync();
    }
}