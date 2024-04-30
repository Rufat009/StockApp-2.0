
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
        return await dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await dbContext.Categories.FindAsync(id);
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
        var category = await dbContext.Categories.FindAsync(id);
        if (category != null)
        {
            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();
        }
    }
}