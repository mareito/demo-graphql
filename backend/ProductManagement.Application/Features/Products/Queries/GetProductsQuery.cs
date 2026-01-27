using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Features.Products.Queries;

public record GetProductsQuery : IRequest<List<ProductDto>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Products
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
            .ToListAsync(cancellationToken);
    }
}
