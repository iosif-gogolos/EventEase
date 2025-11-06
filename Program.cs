// PURPOSE: Application entry point - configures services and starts the Blazor WebAssembly app
// LOCATION: Root of EventEase project folder
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using EventEase;
using EventEase.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// Registers the root App component to render in the #app div in index.html
builder.RootComponents.Add<App>("#app");
// Registers HeadOutlet for dynamic head content management
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configures HttpClient with base address for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Registers EventService for dependency injection throughout the app
builder.Services.AddScoped<IEventService, EventService>();

await builder.Build().RunAsync();