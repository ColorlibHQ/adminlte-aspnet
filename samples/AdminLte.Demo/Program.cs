using AdminLte.Demo.Components;
using ColorlibHQ.AdminLTE.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Razor Components with interactive server render mode.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register AdminLTE services + bind the sidebar menu from appsettings.json ("AdminLte").
builder.Services.AddAdminLte(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

// Serves the RCL static web assets under /_content/ColorlibHQ.AdminLTE.AspNetCore.
app.MapStaticAssets();

app.MapRazorComponents<AdminLte.Demo.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
