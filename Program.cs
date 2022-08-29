using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleApplication.Data;
using SampleApplication;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SampleApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SampleApplicationContext") ?? throw new InvalidOperationException("Connection string 'SampleApplicationContext' not found.")));

var resource = ResourceBuilder.CreateDefault().AddService("AspNet", "Test");

builder.Services.AddOpenTelemetryTracing(b =>
{
    // uses the default Jaeger settings
    b.AddJaegerExporter();

    // receive traces from our own custom sources
    b.AddSource(TelemetryConstants.TestAppSource);

    // decorate our service name so we can find it when we look inside Jaeger
    b.SetResourceBuilder(resource);

    // receive traces from built-in sources
    b.AddHttpClientInstrumentation();
    b.AddAspNetCoreInstrumentation();
    b.AddSqlClientInstrumentation();
});

builder.Services.AddOpenTelemetryMetrics(b =>
{
    // add prometheus exporter
    b.AddPrometheusExporter();

    // receive metrics from our own custom sources
    b.AddMeter(TelemetryConstants.TestAppSource);

    // decorate our service name so we can find it when we look inside Prometheus
    b.SetResourceBuilder(resource);

    // receive metrics from built-in sources
    b.AddHttpClientInstrumentation();
    b.AddAspNetCoreInstrumentation();
});

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

app.Run();
