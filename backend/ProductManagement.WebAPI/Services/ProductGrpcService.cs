using Grpc.Core;
using MediatR;
using ProductManagement.Application.Features.Products.Commands;
using ProductManagement.Application.Features.Products.Queries;
using ProductManagement.WebAPI.Protos;

namespace ProductManagement.WebAPI.Services;

public class ProductGrpcService : ProductService.ProductServiceBase
{
    private readonly IMediator _mediator;

    public ProductGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<ProductListResponse> GetAllProducts(GetAllProductsRequest request, ServerCallContext context)
    {
        var products = await _mediator.Send(new GetProductsQuery());

        var response = new ProductListResponse();
        response.Products.AddRange(products.Select(p => new ProductManagement.WebAPI.Protos.Product
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = (double)p.Price,
            Stock = p.Stock,
            CategoryId = p.CategoryId
        }));

        return response;
    }

    public override async Task<ProductResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(request.Id));

        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID {request.Id} not found"));
        }

        return new ProductResponse
        {
            Product = new ProductManagement.WebAPI.Protos.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            }
        };
    }

    public override async Task<ProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
    {
        var command = new CreateProductCommand(request.Name, request.Description, (decimal)request.Price, request.Stock, request.CategoryId);
        var product = await _mediator.Send(command);

        return new ProductResponse
        {
            Product = new ProductManagement.WebAPI.Protos.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            }
        };
    }

    public override async Task<ProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
    {
        var command = new UpdateProductCommand(request.Id, request.Name, request.Description, (decimal)request.Price, request.Stock, request.CategoryId);
        var product = await _mediator.Send(command);

        if (product == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID {request.Id} not found"));
        }

        return new ProductResponse
        {
            Product = new ProductManagement.WebAPI.Protos.Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = (double)product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            }
        };
    }

    public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
    {
        var success = await _mediator.Send(new DeleteProductCommand(request.Id));

        if (!success)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID {request.Id} not found"));
        }

        return new DeleteProductResponse
        {
            Success = true
        };
    }
}
