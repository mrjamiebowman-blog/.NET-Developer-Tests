﻿using Microsoft.ApplicationInsights.Channel;

namespace MrJB.DeveloperTests.App.Tests.Common.Helpers
{
    public class MockAppInsightsTelemetryChannel : ITelemetryChannel
    {
        public IList<ITelemetry> Items
        {
            get;
            private set;
        }

        public void Send(ITelemetry item)
        {
            Items.Add(item);
        }

        public void Flush()
        {
            throw new NotImplementedException();
        }

        public bool? DeveloperMode { get; set; } = true;

        public string EndpointAddress { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
