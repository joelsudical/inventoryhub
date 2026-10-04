using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClientApp;
using ClientApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "/";
var apiBaseAddress = Uri.TryCreate(apiBaseUrl, UriKind.Absolute, out var absoluteApiBaseAddress)
	&& (absoluteApiBaseAddress.Scheme == Uri.UriSchemeHttp || absoluteApiBaseAddress.Scheme == Uri.UriSchemeHttps)
	? absoluteApiBaseAddress
	: new Uri(new Uri(builder.HostEnvironment.BaseAddress), apiBaseUrl);
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = apiBaseAddress });
builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
