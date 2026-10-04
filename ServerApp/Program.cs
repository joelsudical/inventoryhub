var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddOutputCache();

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseOutputCache();

app.MapGet("/api/productlist", () => new[]
{
    new ProductResponse(1, "Laptop", 1200.50m, 25, new CategoryResponse(101, "Electronics")),
    new ProductResponse(2, "Headphones", 50.00m, 100, new CategoryResponse(102, "Accessories"))
})
    // Copilot suggested caching this stable, public catalog response.
    .CacheOutput(policy => policy.Expire(TimeSpan.FromMinutes(1)));

app.Run();

sealed record ProductResponse(int Id, string Name, decimal Price, int Stock, CategoryResponse Category);

sealed record CategoryResponse(int Id, string Name);
