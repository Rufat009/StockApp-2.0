
using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using StockApp.Data;
using StockApp.Models;
using StockApp.Repositories.Base;

namespace StockApp.Repositories;

public class ProductRepository : IProductsRepository<Product>
{
    private readonly StockAppDbContext dbContext;

    public ProductRepository(StockAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IEnumerable<Product> GetAll()
    {
        return dbContext.Products.Include(p => p.ProductCategories)
                                 .ThenInclude(pc => pc.Category)
                                 .ToList();
    }

    public Product GetById(int id)
    {
        return dbContext.Products.Include(p => p.ProductCategories)
                                 .ThenInclude(pc => pc.Category)
                                 .FirstOrDefault(p => p.Id == id);
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

        if (product != null)
        {
            dbContext.Products.Remove(product);

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchProduct)
    {
        using (var connection = new SqlConnection(App.connectionString))
        {
            await connection.OpenAsync();

            var sql = "SELECT * FROM Products WHERE Name LIKE @Name";
            return (await connection.QueryAsync<Product>(sql, new { Name = $"%{searchProduct}%" })).ToList();
        }
    }

    public async Task<IEnumerable<Product>> FilterByCategoriesAsync(List<int> categoryIds)
    {
        using (var connection = new SqlConnection(App.connectionString))
        {
            await connection.OpenAsync();

            var sql = @"SELECT p.* FROM Products p
            INNER JOIN ProductCategory pc ON p.Id = pc.ProductId
            WHERE pc.CategoryId IN @CategoryIds";

            return (await connection.QueryAsync<Product>(sql, new { CategoryIds = categoryIds })).ToList();
        }
    }
}
