using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TabloBlazorMain.Server.Context;
using TabloBlazorMain.Server.PhoneTask;
using TabloBlazorMain.Server.LastDanceHostedServices;
using TabloBlazorMain.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string con = "Host=192.168.147.69; port=5432; Database=InformationTabloBase; username=postgres; password=nw6Gs79d";
//string con = "Host=localhost; port=5432; Database=InformationTabloBase; username=postgres; password=gaz_gaz_Ilyas12";
//string con = "Host=localhost; port=5432; Database=postgres; username=postgres; password=gaz_gaz_Ilyas12";
// Configure the HTTP request pipeline.

builder.Services.AddDbContext<context>(options => options.UseNpgsql(con).UseLazyLoadingProxies());
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR(p =>
{
    p.EnableDetailedErrors = true;
    p.ClientTimeoutInterval = System.TimeSpan.FromMinutes(1);
    p.HandshakeTimeout = System.TimeSpan.FromSeconds(30);
    p.KeepAliveInterval = System.TimeSpan.FromSeconds(30);
}
);

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddHostedService<MainSheduleHostedService>();
builder.Services.AddHostedService<FloorSheduleLoadHostedService>();
builder.Services.AddHostedService<MyHostedService>();
builder.Services.AddHostedService<WeekNameHostedService>();
builder.Services.AddHostedService<AdminHosted>();
builder.Services.AddHostedService<AnnouncmentHostedService>();
builder.Services.AddHostedService<FullUpdateHostService>();
builder.Services.AddHostedService<DayPartHeadersService>();

builder.Services.AddMemoryCache();
var app = builder.Build();


app.UseResponseCompression();
app.UseDeveloperExceptionPage();

    app.UseWebAssemblyDebugging();


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
    endpoints.MapHub<GoodHubGbl>("/GoodHubGbl");
    endpoints.MapFallbackToFile("index.html");
});

app.Run();
