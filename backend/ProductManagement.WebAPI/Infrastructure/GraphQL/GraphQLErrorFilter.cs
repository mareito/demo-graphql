using Serilog;

namespace ProductManagement.WebAPI.Infrastructure.GraphQL;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception != null)
        {
            Log.Error(error.Exception, "GraphQL Error: {Message}", error.Message);
        }
        else
        {
            Log.Error("GraphQL Error: {Message}", error.Message);
        }

        return error;
    }
}
