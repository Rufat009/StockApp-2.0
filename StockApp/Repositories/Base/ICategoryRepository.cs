using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockApp.Models;

namespace StockApp.Repositories.Base;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task<Category> GetByIdAsync(int id);
    public Task AddAsync(Category category);
    public Task UpdateAsync(Category category);
    public Task DeleteAsync(int id);

}

