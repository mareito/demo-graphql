using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Features.Products.Commands;

public record CreateProductCommand(string Name, string Description, decimal Price, int Stock, int CategoryId) : IRequest<ProductDto>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        var category = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);

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
