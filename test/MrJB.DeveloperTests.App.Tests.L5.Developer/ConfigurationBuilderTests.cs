using System.Text.Json;
using System.Text.Json.Serialization;
using MrJB.DeveloperTests.App.Configuration;
using Newtonsoft.Json.Linq;

namespace MrJB.DeveloperTests.App.Tests.L5.Developer;

public class ConfigurationBuilderTests
{
    private string _loggingJson = @"
    {
      'Logging': {
            'LogLevel': {
                'Default': 'Information',
                'Microsoft': 'Warning',
                'Microsoft.Hosting.Lifetime': 'Information'
            }
        },
        'AllowedHosts': '*'
    }";

    [Fact]
    public async Task Consumer_Configuration_Test()
    {
        // consumer settings
        var consumerSettings = new ConsumerSettings();

        // sql connection
        consumerSettings.SqlDatabase = new DatabaseConfiguration();
        consumerSettings.SqlDatabase.ConnectionString = "connection-string";

        // azure service bus
        consumerSettings.AzureServiceBus = new ConsumerSettings.AzureServiceBusConfiguration();
        consumerSettings.AzureServiceBus.ConnectionString = "connection-string";
        consumerSettings.AzureServiceBus.QueueOrTopic = "integrationevents-admin";

        // down stream services
        consumerSettings.DownStreamServices = new ConsumerSettings.DownStreamServicesConfiguration();
        consumerSettings.DownStreamServices.Designers = "integrationevents-designers";
        consumerSettings.DownStreamServices.Fulfillment = "integrationevents-fulfillment";

        //// rollbar configuration
        //RollbarConfiguration rollbarConfiguraiton = new RollbarConfiguration();
        //rollbarConfiguraiton.Environment = "Dev"; // Production
        //rollbarConfiguraiton.AccessToken = "";

        // config
        var config = new
        {
            Consumer = consumerSettings,
            //Rollbar = rollbarConfiguraiton
        };

        // json
        var options = new JsonSerializerOptions
        {
            Converters = {
                    new JsonStringEnumConverter()
                }
        };

        // serialize
        var json = JsonSerializer.Serialize(config, options);

        // merge json
        JObject loggingConfig = JObject.Parse(_loggingJson);
        JObject configJson = JObject.Parse(json);

        loggingConfig.Merge(configJson, new JsonMergeSettings()
        {
            MergeArrayHandling = MergeArrayHandling.Union
        });
        json = loggingConfig.ToString();

        // output
        Console.WriteLine(json);

        // path (root of project)
        var path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, "appSettings.json");

        // create file
        await File.WriteAllTextAsync(path, json);

        // output
        Console.WriteLine(json);
    }
}
