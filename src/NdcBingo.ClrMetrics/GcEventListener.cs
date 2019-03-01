using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using App.Metrics;

namespace NdcBingo.ClrMetrics
{
    public class GcEventListener : EventListener
    {
        private readonly IMetrics _metrics;
        private readonly HashSet<string> _seen = new HashSet<string>();

        public GcEventListener(IMetrics metrics)
        {
            _metrics = metrics;
        }

        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name.Equals("Microsoft-Windows-DotNETRuntime"))
            {
                EnableEvents(eventSource, EventLevel.Verbose, (EventKeywords)(-1));
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            switch ((EventIds)eventData.EventId)
            {
                case EventIds.ThreadPoolWorkerThreadAdjustmentAdjustment:
                    var newWorkerThreadCount = (uint) eventData.Payload[1];
                    _metrics.SetThreadCount(newWorkerThreadCount);
                    break;
                case EventIds.ExceptionThrownV1:
                    _metrics.ExceptionThrown(eventData.Payload[0] as string);
                    break;
            }
        }
    }
}
