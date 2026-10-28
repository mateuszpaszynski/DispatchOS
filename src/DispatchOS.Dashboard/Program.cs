using DispatchOS.Dashboard.Components;
using DispatchOS.Dashboard.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

if (builder.Configuration.GetValue<bool>("UseFakeApi"))
{
    builder.Services.AddSingleton<IDispatchApiClient, FakeDispatchApiClient>();
}
else
{
    builder.Services.AddHttpClient<IDispatchApiClient, HttpDispatchApiClient>(c =>
        c.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!));
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
