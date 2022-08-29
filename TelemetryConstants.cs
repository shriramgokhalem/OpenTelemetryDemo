using System.Diagnostics;
using System.Diagnostics.Metrics;
using OpenTelemetry.Trace;

namespace SampleApplication;

public static class TelemetryConstants
{
    /// <summary>
    /// The name of the <see cref="ActivitySource"/> that is going to produce our traces and
    /// the <see cref="Meter"/> that is going to produce our metrics.
    /// </summary>
    public const string TestAppSource = "Demo.AspNet";

    public static readonly ActivitySource DemoTracer = new ActivitySource(TestAppSource);

    public static readonly Meter DemoMeter = new Meter(TestAppSource);

    public static readonly Counter<long> HitsCounter =
        DemoMeter.CreateCounter<long>("IndexHits", "hits", "number of hits to homepage");
}