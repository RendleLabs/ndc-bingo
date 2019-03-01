using App.Metrics;
using App.Metrics.Counter;

namespace NdcBingo.Data
{
    public static class PlayerMetrics
    {
        private static readonly CounterOptions Players = new CounterOptions
        {
            Name = "players",
            MeasurementUnit = Unit.Items
        };

        public static void SetPlayerCount(this IMetrics metrics, int count)
        {
            metrics.Provider.Counter.Instance(Players).Reset();
            metrics.Measure.Counter.Increment(Players, count);
        }
    }
}