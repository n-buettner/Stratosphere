using Microsoft.EntityFrameworkCore;
using Serilog;
using Stratosphere.Data;
using Stratosphere.Pages.Maintenance;
using Stratosphere.Pages.Monitoring;
using Stratosphere.Pages.Queues;
using Stratosphere.Pages.Queues.Models;
using Stratosphere.Services.Http;
using Stratosphere.Services.Queues;

var builder = WebApplication.CreateBuilder(args);

//dummy update for nuget packages

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders()
        .AddSerilog(new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("Logs/stratosphere.log", rollOnFileSizeLimit: true, fileSizeLimitBytes: 1000000) //1MB
            .CreateLogger());
});

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IHttpService, HttpService>();

builder.Services.Configure<MessageQueueApiSettings>(builder.Configuration.GetSection("MessageQueueApiSettings"));
builder.Services.AddSingleton<IQueueApiService, QueueApiService>();

builder.Services.AddDbContext<StratosphereContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
        .UseSnakeCaseNamingConvention()
        .EnableSensitiveDataLogging();
});

//add services for each page using extension methods
builder.Services.AddMaintenanceServices();
builder.Services.AddMonitoringServices();
builder.Services.AddQueueServices();

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

app.UseAuthorization();

app.MapRazorPages();

//lol, figure out migrations instead of this
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<StratosphereContext>();
    await ctx.Database.EnsureDeletedAsync();
    await ctx.Database.EnsureCreatedAsync();
}

app.Run();
