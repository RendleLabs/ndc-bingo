using App.Metrics;
using App.Metrics.Gauge;
using App.Metrics.Meter;

namespace NdcBingo.ClrMetrics
{
    internal static class ExceptionMetrics
    {
        private static readonly MeterOptions Exceptions = new MeterOptions
        {
            Name = "clr_exceptions_thrown",
            MeasurementUnit = Unit.Errors,
        };

        public static void ExceptionThrown(this IMetrics metrics, string type)
        {
            metrics.Measure.Meter.Mark(Exceptions, new MetricTags("exception_type", type ?? "Unknown"));
        }
    }

    internal static class ProcessMetrics
    {
        private static readonly GaugeOptions PhysicalMemory = new GaugeOptions
        {
            Name = "physical_memory",
            MeasurementUnit = Unit.Bytes
        };

        public static void SetPhysicalMemory(this IMetrics metrics, long memory)
        {
            metrics.Measure.Gauge.SetValue(PhysicalMemory, memory);
        }
    }
}