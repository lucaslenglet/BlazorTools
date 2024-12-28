using BlazorTools.Demo.Components;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

{
    Log.Logger.Information("Application building...");

    builder.Logging
    .ClearProviders()
    .AddSerilog();

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
}

var app = builder.Build();

{
    Log.Logger.Information("Application builded.");

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseAntiforgery();

    app.MapStaticAssets();
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();
}

try
{
    Log.Logger.Information("Executing application...");

    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Application crashed !");
}

Log.Logger.Information("Application stopping...");

Log.CloseAndFlush();