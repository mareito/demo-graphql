using ProductManagement.Application.GraphQL.Mutations;
using ProductManagement.Application.GraphQL.Queries;
using ProductManagement.Infrastructure;
using ProductManagement.Application;

var builder = WebApplication.CreateBuilder(args);

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
    .AddMutationType<ProductMutations>();

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseCors("AllowFrontend");

// Map GraphQL endpoint
app.MapGraphQL();
app.MapGrpcService<ProductManagement.WebAPI.Services.ProductGrpcService>();

app.Run();
