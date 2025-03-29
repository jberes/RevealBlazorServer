using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using RevealBlazorServer;
using RevealBlazorServer.AcmeAnalyticsServer;
using RevealBlazorServer.NorthwindCloud;
using RevealBlazorServer.State;
using IgniteUI.Blazor.Controls;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();


builder.Services.AddScoped<IAcmeAnalyticsServerService, AcmeAnalyticsServerService>();
builder.Services.AddScoped<INorthwindCloudService, NorthwindCloudService>();
builder.Services.AddScoped<IStateService, StateService>();
RegisterIgniteUI(builder.Services);

void RegisterIgniteUI(IServiceCollection services)
{
    services.AddIgniteUIBlazor(
        typeof(IgbNavbarModule),
        typeof(IgbIconButtonModule),
        typeof(IgbRippleModule),
        typeof(IgbNavDrawerModule),
        typeof(IgbComboModule),
        typeof(IgbButtonModule),
        typeof(IgbListModule),
        typeof(IgbAvatarModule)
    );
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
