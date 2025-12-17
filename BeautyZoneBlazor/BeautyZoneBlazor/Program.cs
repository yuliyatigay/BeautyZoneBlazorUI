using BeautyZoneBlazor;
using BeautyZoneBlazor.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);
// Add services to the container.

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5270);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();