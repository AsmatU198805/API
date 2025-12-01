using Radzen;
using System.Net;
using System.Net.Http;
using Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Razor setup
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Allow self-signed SSL certificates (for development)
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:5104/api/");
});



builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

builder.Services.AddRadzenComponents();
var app = builder.Build();

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
