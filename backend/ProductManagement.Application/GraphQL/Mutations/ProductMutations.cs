using HotChocolate;
using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.Products.Commands;

namespace ProductManagement.Application.GraphQL.Mutations;

public class ProductMutations
{
    [GraphQLName("addProduct")]
    public async Task<ProductDto> AddProduct(
        [GraphQLName("input")] AddProductInput input,
        [Service] IMediator mediator)
    {
        var command = new CreateProductCommand(input.Name, input.Description, input.Price, input.Stock, input.CategoryId);
        return await mediator.Send(command);
    }
}
