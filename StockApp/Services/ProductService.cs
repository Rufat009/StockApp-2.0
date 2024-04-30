using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockApp.Models;
using StockApp.Repositories.Base;
using StockApp.Services.Base;

namespace StockApp.Services;

public class ProductService : IProductService
{
    private readonly IProductsRepository productRepository;

    public ProductService(IProductsRepository productRepository)
    {
        this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await productRepository.GetAll();
    }

    public async Task AddProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        await productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        if (product.Id <= 0)
        {
            throw new ArgumentException($"'{nameof(product.Id)}' cannot be negative or 0");
        }

        await productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be negative or 0");
        }

        await productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> SearchProductAsync(string searchProduct)
    {
        if (string.IsNullOrWhiteSpace(searchProduct))
        {
            throw new ArgumentException($"'{nameof(searchProduct)}' cannot be null or whitespace" );
        }

        return await productRepository.SearchAsync(searchProduct);
    }

    public async Task<IEnumerable<Product>> FilterProductsByCategoriesAsync(List<int> categoryIds)
    {
        if (categoryIds == null || !categoryIds.Any())
        {
            throw new ArgumentException($"'{nameof(categoryIds)}' cannot be null or empty");
        }

        return await productRepository.FilterByCategoriesAsync(categoryIds);
    }
}