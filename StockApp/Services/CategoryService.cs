using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockApp.Models;
using StockApp.Repositories.Base;

namespace StockApp.Services;

public class CategoryService
{
    private readonly ICategoryRepository categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be negative or 0");
        }

        return await categoryRepository.GetByIdAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        await categoryRepository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        if (category.Id <= 0)
        {
            throw new ArgumentException($"'{nameof(category.Id)}' cannot be negative or 0");
        }

        await categoryRepository.UpdateAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be negative or 0");
        }

        await categoryRepository.DeleteAsync(id);
    }
}
