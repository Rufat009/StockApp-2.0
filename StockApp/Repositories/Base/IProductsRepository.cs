using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockApp.Models;

namespace StockApp.Repositories.Base;


public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetAll();

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(int id);

    Task<IEnumerable<Product>> SearchAsync(string searchProduct);

    public  Task<IEnumerable<Product>> FilterByCategoriesAsync(List<int> categoryIds);
}
