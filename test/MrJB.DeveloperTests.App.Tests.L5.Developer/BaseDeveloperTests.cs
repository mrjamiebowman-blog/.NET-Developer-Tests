using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using MrJB.DeveloperTests.App.AutoMapper;
using MrJB.DeveloperTests.App.Configuration;

namespace MrJB.DeveloperTests.App.Tests.L5.Developer;

public abstract class BaseDeveloperTest
{
    // auto-mapper
    protected IMapper _autoMapper;

    // app insights
    protected TelemetryClient _telemetryClient;

    // configuration
    protected ConsumerSettings _consumerSettings;

    public BaseDeveloperTest()
    {
        LoadAppSettingsAsync().Wait();
        GetSetAutoMapperAsync().Wait();
        GetSetAppInsightsAsync().Wait();
    }

    protected Task LoadAppSettingsAsync()
    {
        // configuration
        var configurationBuilder = new ConfigurationBuilder();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "appSettings.json");
        configurationBuilder.AddJsonFile(path, false);

        // build
        var root = configurationBuilder.Build();

        // set configuration
        _consumerSettings = new ConsumerSettings();
        root.GetSection(ConsumerSettings.Position).Bind(_consumerSettings);

        return Task.CompletedTask;
    }

    protected Task<IMapper> GetSetAutoMapperAsync()
    {
        // auto mapper
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<DataServiceMappingProfiles>()
        );

        // mapper 
        _autoMapper = new Mapper(config);
        return Task.FromResult(_autoMapper);
    }

    protected Task<TelemetryClient> GetSetAppInsightsAsync(Action<TelemetryConfiguration> configAction = null)
    {
        TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();
        configuration.InstrumentationKey = "";
        configAction?.Invoke(configuration);

        var telemetryClient = new TelemetryClient(configuration);
        _telemetryClient = telemetryClient;
        return Task.FromResult(telemetryClient);
    }
}

