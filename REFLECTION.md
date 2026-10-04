# Copilot Reflection

Copilot helped build the InventoryHub integration in stages. It connected the Blazor WebAssembly page to the Minimal API, corrected the client/server route mismatch, and added CORS so the client could make a cross-origin request. It also helped structure the response as typed product and category records, with camel-case JSON fields and decimal prices.

The main debugging challenge was that a client request can fail for different reasons: a wrong route, a CORS policy mismatch, a network failure, or invalid JSON. Keeping those cases visible in the page made the integration easier to check. The final response was verified directly at `/api/productlist`, and the browser rendered both products and categories without console errors.

For performance, Copilot suggested a one-minute server output cache and a scoped client cache that reuses an in-flight request and successful results. A browser request-count check showed two API requests across two product-page visits before client caching and one afterward. The API data is only two static products, so the test demonstrates reduced repeat calls rather than a meaningful production throughput benchmark.

I learned to use Copilot iteratively: state the expected contract, inspect generated code, test actual HTTP responses, and measure behavior before and after optimization. Copilot accelerated implementation and surfaced useful patterns, while builds and browser checks remained necessary to validate its suggestions against the running application.