
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

            var sql = @"SELECT p.*, c.* 
                    FROM Products p
                    LEFT JOIN ProductCategories pc ON p.Id = pc.ProductId
                    LEFT JOIN Categories c ON pc.CategoryId = c.Id
                    WHERE p.Name LIKE @Name";

            var productDictionary = new Dictionary<int, Product>();

            var list = await connection.QueryAsync<Product, Category, Product>(
                sql,
                (product, category) =>
                {
                    Product productEntry;

                    if (!productDictionary.TryGetValue(product.Id, out productEntry))
                    {
                        productEntry = product;
                        productEntry.ProductCategories = new List<ProductCategory>();
                        productDictionary.Add(productEntry.Id, productEntry);
                    }

                    productEntry.ProductCategories.Add(new ProductCategory { Category = category });
                    return productEntry;
                },
                new { Name = $"%{searchProduct}%" },
                splitOn: "Id");

            return list.Distinct();
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
