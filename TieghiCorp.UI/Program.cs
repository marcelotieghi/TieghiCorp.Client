using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TieghiCorp.UI;
using TieghiCorp.UI.Core.Models;
using TieghiCorp.UI.Core.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder
    .RootComponents
    .Add<App>("#app");

builder
    .RootComponents
    .Add<HeadOutlet>("head::after");

builder
    .Services
    .AddMudServices();

builder
    .Services
    .AddScoped<IServices<Location>, Services<Location>>()
    .AddScoped<IServices<Department>, Services<Department>>()
    .AddScoped<IServices<Personnel>, Services<Personnel>>();

builder
    .Services
    .AddScoped(_ => new HttpClient
    {
        BaseAddress = new Uri("https://tieghicorp-app-a4gfamagbwc6b0hb.westeurope-01.azurewebsites.net/")
    });

await builder
    .Build()
    .RunAsync();