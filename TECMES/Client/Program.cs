global using Microsoft.AspNetCore.Components.Authorization;
global using TECMES.Shared.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using TECMES.Client;
using TECMES.Client.Authentication;
using TECMES.Client.Services.OrdemProducaoServices;
using TECMES.Client.Services.PedidoServices;
using TECMES.Client.Services.ProducaoServices;
using TECMES.Client.Services.ProdutoServices;
using TECMES.Client.Services.ReloadServices;
using TECMES.Client.Services.UsuarioServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IOrdemProducaoService, OrdemProducaoService>();
builder.Services.AddScoped<IProducaoService, ProducaoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IReloadService, ReloadService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
