# .NET: Developer Tests
This project will contain an abstract class and sample of how to use this class for building developer tests. I use something similar to this in every project for unit and developer ran integration tests for taking a test driven'ish approach. I'll use this repo to manage these since they evolved over time. The utility helper classes can also be used in unit testing projects.   

## App Packages (UI)
Application Insights (Worker)

AutoMapper


## Packages

Moq

Fluent Assertions

## Git Ignore (appSettings.json, launchSettings.json)
```json
# appsettings
**/appSettings.json
**/appSettings.*.json

# launchsettings
**/launchSettings.json
```

## Common (Utility Helpers)
These helpers can be used to help troubleshoot and test services. They are more geared towards unit testing projects but can also be used as a substitute in a developer test.

### Mock Logger
If you need to verify that a message is being logged this utility class can be used.

`var logger = new MockLogger<AzureServiceBusService>();`


