using MediatR;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Application.Features.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest<bool>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);

        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
