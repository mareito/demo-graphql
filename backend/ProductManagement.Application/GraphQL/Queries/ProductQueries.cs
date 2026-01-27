using HotChocolate;
using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.Products.Queries;

namespace ProductManagement.Application.GraphQL.Queries;

public class ProductQueries
{
    [GraphQLName("getProducts")]
    public async Task<List<ProductDto>> GetProducts([Service] IMediator mediator)
    {
        return await mediator.Send(new GetProductsQuery());
    }
}
