using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PTO_WebAdmin;
using PTO_WebAdmin.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("PlayTogether.Api", client =>
{
    client.BaseAddress = new Uri("https://play-together.azurewebsites.net/swagger/play-together/");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddTransient<AuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("PlayTogether.Api"));

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

builder.Services.AddHttpClientServices();


await builder.Build().RunAsync();
