using ProductManagement.Application.GraphQL.Mutations;
using ProductManagement.Application.GraphQL.Queries;
using ProductManagement.Infrastructure;
using ProductManagement.Application;
using Serilog;
using ProductManagement.WebAPI.Infrastructure.Middleware;
using ProductManagement.WebAPI.Infrastructure.GraphQL;
using ProductManagement.WebAPI.Infrastructure.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// Add Infrastructure services (EF Core, PostgreSQL)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// Add CORS policy for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Vite default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add GraphQL with HotChocolate
builder.Services
    .AddGraphQLServer()
    .AddQueryType<ProductQueries>()
    .AddMutationType<ProductMutations>()
    .AddErrorFilter<GraphQLErrorFilter>(); // Register GraphQL Error Filter

// Add gRPC with Interceptor
builder.Services.AddGrpc(options =>
{
    options.Interceptors.Add<GrpcExceptionInterceptor>();
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<GlobalExceptionMiddleware>(); // Global Exception Middleware
app.UseSerilogRequestLogging(); // Log HTTP requests

app.UseCors("AllowFrontend");

// Map GraphQL endpoint
app.MapGraphQL();
app.MapGrpcService<ProductManagement.WebAPI.Services.ProductGrpcService>();

app.Run();
