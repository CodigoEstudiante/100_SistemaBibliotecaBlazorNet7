using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SistemaBiblioteca.Client;
using SistemaBiblioteca.Client.Servicios.Contrato;
using SistemaBiblioteca.Client.Servicios.Implementacion;
using SistemaBiblioteca.Client.Utilidad;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddSingleton<MenuService>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<ILectorServicio, LectorServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<ILibroServicio, LibroServicio>();
builder.Services.AddScoped<IPrestamoServicio, PrestamoServicio>();
builder.Services.AddScoped<IDashBoardServicio, DashBoardServicio>();


builder.Services.AddSweetAlert2();
await builder.Build().RunAsync();
