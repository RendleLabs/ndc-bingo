using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.Extensions.Hosting;

namespace NdcBingo.ClrMetrics
{
    public class ClrMetricsService : IHostedService
    {
        private static readonly TimeSpan TimerInterval = TimeSpan.FromSeconds(5);
        private readonly IMetrics _metrics;
        private GcEventListener _gcEventListener;
        private readonly Process _process;
        private readonly Timer _timer;

        public ClrMetricsService(IMetrics metrics)
        {
            _metrics = metrics;
            _process = Process.GetCurrentProcess();
            _timer = new Timer(OnTimer);
        }

        private void OnTimer(object state)
        {
            _metrics.SetPhysicalMemory(_process.WorkingSet64);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer.Change(TimerInterval, TimerInterval);
            _gcEventListener = new GcEventListener(_metrics);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _gcEventListener.Dispose();
            return Task.CompletedTask;
        }
    }
}