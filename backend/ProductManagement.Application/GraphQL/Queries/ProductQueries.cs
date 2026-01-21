using HotChocolate;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.GraphQL.Queries;

public class ProductQueries
{
    [GraphQLName("getProducts")]
    public async Task<List<ProductDto>> GetProducts([Service] IApplicationDbContext context)
    {
        var products = await context.Products
            .Include(p => p.Category)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                Category = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Description = p.Category.Description
                }
            })
            .ToListAsync();

        return products;
    }
}
