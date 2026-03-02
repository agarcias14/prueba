using PruebaTecnica.Ui.Components;
using PruebaTecnica.Ui.Services;

var builder = WebApplication.CreateBuilder(args);

// Servicios de Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar HttpClient para consumir la API
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5100");
});

// Registrar servicio que consume la API
builder.Services.AddScoped<ProductosApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// ⚠️ CRÍTICO: Archivos estáticos (CSS, JS)
app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();