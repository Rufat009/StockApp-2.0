using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockApp.Models;

namespace StockApp.Repositories.Base;


public interface IProductsRepository<TEntity>
{
    IEnumerable<TEntity> GetAll();

    Task AddAsync(TEntity product);

    Task UpdateAsync(TEntity product);

    Task DeleteAsync(int id);

    Task<IEnumerable<Product>> SearchAsync(string searchProduct);

    public  Task<IEnumerable<Product>> FilterByCategoriesAsync(List<int> categoryIds);
}
