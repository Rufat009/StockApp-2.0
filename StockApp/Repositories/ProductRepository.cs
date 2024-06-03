
using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockApp.Data;
using StockApp.Models;
using StockApp.Repositories.Base;

namespace StockApp.Repositories;

public class ProductRepository : IProductsRepository
{
    private readonly StockAppDbContext dbContext;

    public ProductRepository(StockAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await dbContext.Products.Include(p => p.ProductCategories)
                                 .ThenInclude(pc => pc.Category)
                                 .ToListAsync();
    }

    public async Task<Product> GetById(int id)
    {
        return await dbContext.Products.Include(p => p.ProductCategories)
                                 .ThenInclude(pc => pc.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync();

    }

    public async Task UpdateAsync(Product product)
    {
        dbContext.Products.Update(product);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = dbContext.Products.FirstOrDefault(p => p.Id == id);


        dbContext.Products.Remove(product!);

        await dbContext.SaveChangesAsync();

    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchProduct)
    {
        var products = await dbContext.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).Where(p => p.Name.Contains(searchProduct)).ToListAsync();

        return products;
    }

    public async Task<IEnumerable<Product>> FilterByCategoriesAsync(List<int> categoryIds)
    {
        var connection = new SqlConnection(App.connectionString);
        await connection.OpenAsync();

        var sql = @"SELECT p.* FROM Products p
                    INNER JOIN ProductCategory pc ON p.Id = pc.ProductId
                    WHERE pc.CategoryId IN @CategoryIds";

        var products = (await connection.QueryAsync<Product>(sql, new { CategoryIds = categoryIds })).ToList();

        connection.Close();

        return products;
    }
}
