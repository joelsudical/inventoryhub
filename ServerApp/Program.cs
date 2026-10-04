var builder = WebApplication.CreateBuilder(args);

if (int.TryParse(Environment.GetEnvironmentVariable("PORT"), out var port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

builder.Services.AddCors();
builder.Services.AddOutputCache();

var app = builder.Build();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseCors(policy =>
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
app.UseOutputCache();

app.MapGet("/healthz", () => TypedResults.Ok(new { status = "ok" }));

app.MapGet("/api/productlist", () => new[]
{
    new ProductResponse(1, "Laptop", 1200.50m, 25, new CategoryResponse(101, "Electronics")),
    new ProductResponse(2, "Headphones", 50.00m, 100, new CategoryResponse(102, "Accessories"))
})
    // Copilot suggested caching this stable, public catalog response.
    .CacheOutput(policy => policy.Expire(TimeSpan.FromMinutes(1)));

app.MapFallbackToFile("index.html");

app.Run();

sealed record ProductResponse(int Id, string Name, decimal Price, int Stock, CategoryResponse Category);

sealed record CategoryResponse(int Id, string Name);
