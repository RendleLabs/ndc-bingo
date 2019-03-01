using System;
using App.Metrics;
using App.Metrics.Timer;

namespace NdcBingo.Data
{
    public static class DatabaseMetrics
    {
        private static readonly TimerOptions PlayerQuery = new TimerOptions
        {
            Name = "db_player_query",
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Minutes
        };

        public static IDisposable PlayerQueryTimer(this IMetrics metrics, string by) =>
            metrics.Measure.Timer.Time(PlayerQuery, new MetricTags("by", by));

        private static readonly TimerOptions PlayerCreate = new TimerOptions
        {
            Name = "db_player_create",
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Minutes
        };

        public static IDisposable PlayerCreateTimer(this IMetrics metrics) =>
            metrics.Measure.Timer.Time(PlayerCreate);

        private static readonly TimerOptions PlayerCountQuery = new TimerOptions
        {
            Name = "db_player_count_query",
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Minutes
        };

        public static IDisposable PlayerCountQueryTimer(this IMetrics metrics) =>
            metrics.Measure.Timer.Time(PlayerCountQuery);

        private static readonly TimerOptions PlayerIdDuplicateCheck = new TimerOptions
        {
            Name = "db_player_query",
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Minutes
        };

        public static IDisposable PlayerIdDuplicateCheckTimer(this IMetrics metrics) =>
            metrics.Measure.Timer.Time(PlayerIdDuplicateCheck);
        
        private static readonly TimerOptions RandomSquaresQuery = new TimerOptions
        {
            Name = "db_random_squares_query",
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Minutes
        };

        public static IDisposable RandomSquaresQueryTimer(this IMetrics metrics) =>
            metrics.Measure.Timer.Time(PlayerIdDuplicateCheck);
        
        private static readonly TimerOptions SquaresQuery = new TimerOptions
        {
            Name = "db_squares_query",
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Minutes
        };

        public static IDisposable SquaresQueryTimer(this IMetrics metrics) =>
            metrics.Measure.Timer.Time(PlayerIdDuplicateCheck);
    }
}