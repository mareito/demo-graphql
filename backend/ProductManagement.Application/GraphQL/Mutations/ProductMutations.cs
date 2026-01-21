using HotChocolate;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.GraphQL.Mutations;

public class ProductMutations
{
    [GraphQLName("addProduct")]
    public async Task<ProductDto> AddProduct(
        [GraphQLName("input")] AddProductInput input,
        [Service] IApplicationDbContext context)
    {
        var product = new Product
        {
            Name = input.Name,
            Description = input.Description,
            Price = input.Price,
            Stock = input.Stock,
            CategoryId = input.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        // Load the category for the response
        var category = await context.Categories.FindAsync(product.CategoryId);

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            Category = category != null ? new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            } : null
        };
    }
}
