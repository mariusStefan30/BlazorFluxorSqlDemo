
//using BlazorFluxorSqlDemo.Data;         // pentru AppDbContext / ItemsService
//using Microsoft.EntityFrameworkCore;
//using Fluxor;
//using Fluxor.Blazor.Web.ReduxDevTools;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Configuration;



//var builder = WebApplication.CreateBuilder(args);

////EF Core
//builder.Services.AddDbContext<AppDbContext>(o =>
//	o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
////Blazor
//builder.Services.AddRazorPages();
//builder.Services.AddServerSideBlazor();
////Fluxor
//builder.Services.AddFluxor(opt =>
//{
//    opt.ScanAssemblies(typeof(Program).Assembly);
//#if DEBUG
//    opt.UseReduxDevTools();
//#endif
//});
////app service
//builder.Services.AddScoped<ItemsService>();

//var app = builder.Build();

//if (!app.Environment.IsDevelopment())
//{
//	app.UseExceptionHandler("/Error", createScopeForErrors: true);
//	app.UseHsts();
//}

//app.UseStaticFiles();
//app.UseRouting();
//app.MapBlazorHub();
//app.MapFallbackToPage("/Home");
//app.Run();

using BlazorFluxorSqlDemo.Data;         // pentru AppDbContext / ItemsService
using Microsoft.EntityFrameworkCore;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using BlazorFluxorSqlDemo.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluxor(opt =>
{
    opt.ScanAssemblies(typeof(Program).Assembly);
#if DEBUG
    opt.UseReduxDevTools();
#endif
});

builder.Services.AddScoped<ItemsService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// ✅ acesta este endpoint-ul corect în .NET 8
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
