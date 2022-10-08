namespace MrJB.DeveloperTests.App.Configuration;

public class ConsumerSettings
{
    public const string Position = "Consumer";

    public AzureServiceBusConfiguration AzureServiceBus { get; set; } = new AzureServiceBusConfiguration();

    public DatabaseConfiguration SqlDatabase { get; set; } = new DatabaseConfiguration();

    public DownStreamServicesConfiguration DownStreamServices { get; set; } = new DownStreamServicesConfiguration();

    public class AzureServiceBusConfiguration
    {
        public const string Position = $"{ConsumerSettings.Position}:AzureServiceBus";

        public string ConnectionString { get; set; }

        public string QueueOrTopic { get; set; }

        public string SubscriptionName { get; set; }
    }

    public class DownStreamServicesConfiguration
    {
        public string Designers { get; set; }

        public string Fulfillment { get; set; }
    }
}
