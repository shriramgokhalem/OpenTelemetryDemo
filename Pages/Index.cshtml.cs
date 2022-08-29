using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SqlServer.Management.Sdk.Differencing.SPI;
using OpenTelemetry.Trace;
using System.Diagnostics;
using static SampleApplication.TelemetryConstants;

namespace SampleApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Tracer _tracer;

        public IndexModel(ILogger<IndexModel> logger, TracerProvider provider)
        {
            _logger = logger;
            _tracer = provider.GetTracer(TelemetryConstants.TestAppSource);
        }

        public void OnGet()
        {
            var tags = new TagList();
            tags.Add("user-agent", Request.Headers.UserAgent);
            HitsCounter.Add(1, tags);
            using var mySpan = _tracer.StartActiveSpan("Home").SetAttribute("httpTracer", HttpContext.TraceIdentifier);
            mySpan.AddEvent($"Received HTTP request from {Request.Headers.UserAgent}");

        }
    }
}