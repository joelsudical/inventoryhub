# InventoryHub

InventoryHub is a Blazor WebAssembly client backed by an ASP.NET Core Minimal API. The client requests `GET /api/productlist` and displays each product's name, price, stock, and nested category.

## Run locally

From the solution root, start each project in a separate terminal:

```powershell
dotnet run --project ServerApp/ServerApp.csproj --launch-profile http
dotnet run --project ClientApp/ClientApp.csproj --launch-profile http
```

Open `http://localhost:5071/fetchproducts`. The API is available at `http://localhost:5068/api/productlist`.

## Integration and performance

- The API returns typed product and category records; ASP.NET Core serializes them as camel-case JSON.
- The API output cache keeps this public, read-only response for one minute.
- The client-scoped `ProductService` reuses an in-flight request and caches successful data for one minute. Failed requests are not cached, so the page can retry when revisited.
- CORS is enabled for the development integration.

## Copilot contribution

Copilot helped generate the client/API integration, identify and correct the route mismatch, configure CORS, shape the nested JSON response, and suggest the one-minute API and client caches. The source comments mark the caching decisions. The client request-count check measured two API requests across two page visits before client caching and one afterward. This small sample demonstrates reduced repeated requests; it is not a production load benchmark.