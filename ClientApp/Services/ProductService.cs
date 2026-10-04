using System.Net.Http.Json;
using System.Text.Json;
using ClientApp.Models;

namespace ClientApp.Services;

public sealed class ProductService(HttpClient httpClient)
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(1);
    private Product[]? cachedProducts;
    private DateTimeOffset cacheExpiresAt;
    private Task<Product[]>? pendingRequest;

    public Task<Product[]> GetProductsAsync()
    {
        if (cachedProducts is not null && DateTimeOffset.UtcNow < cacheExpiresAt)
        {
            return Task.FromResult(cachedProducts);
        }

        // Copilot suggested reusing the in-flight request and cached result across page visits.
        return pendingRequest ??= FetchProductsAsync();
    }

    private async Task<Product[]> FetchProductsAsync()
    {
        try
        {
            var products = await httpClient.GetFromJsonAsync<Product[]>("api/productlist")
                ?? throw new JsonException("The product API returned an empty response.");

            cachedProducts = products;
            cacheExpiresAt = DateTimeOffset.UtcNow.Add(CacheDuration);
            return products;
        }
        finally
        {
            pendingRequest = null;
        }
    }
}