using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog;

namespace ProductManagement.WebAPI.Infrastructure.Grpc;

public class GrpcExceptionInterceptor : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "gRPC Error in {Method}", context.Method);
            
            // Map common exceptions to gRPC status codes
            var status = ex switch
            {
                ArgumentException => new Status(StatusCode.InvalidArgument, ex.Message),
                KeyNotFoundException => new Status(StatusCode.NotFound, ex.Message),
                _ => new Status(StatusCode.Internal, "An external error occurred")
            };

            throw new RpcException(status);
        }
    }
}
