using App.Metrics;
using App.Metrics.Gauge;

namespace NdcBingo.ClrMetrics
{
    internal static class ThreadPoolMetrics
    {
        private static readonly GaugeOptions ThreadCount = new GaugeOptions
        {
            Name = "clr_threadpool_thread_count",
            MeasurementUnit = Unit.Threads
        };

        public static void SetThreadCount(this IMetrics metrics, uint threadCount)
        {
            metrics.Measure.Gauge.SetValue(ThreadCount, threadCount);
        }
    }
}