using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Features.Products.Commands;

public record UpdateProductCommand(int Id, string Name, string Description, decimal Price, int Stock, int CategoryId) : IRequest<ProductDto?>;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
{
    private readonly IApplicationDbContext _context;

    public UpdateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);

        if (product == null) return null;

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.CategoryId = request.CategoryId;
        
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
