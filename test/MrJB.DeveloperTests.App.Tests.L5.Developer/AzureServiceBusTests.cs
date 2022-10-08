using Azure.Messaging.ServiceBus;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace MrJB.DeveloperTests.App.Tests.L5.Developer
{
    public class AzureServiceBusTests : BaseDeveloperTest
    {
        private readonly ITestOutputHelper _output;
        private string _queueOrTopic = "orders";

        public AzureServiceBusTests(ITestOutputHelper output) : base()
        {
            _output = output;
        }

        public async Task Publish<T>(T model)
        {
            try
            {
                // json
                var serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                var json = JsonSerializer.Serialize(model, serializerOptions);
                _output.WriteLine(json);

                await using (ServiceBusClient client = new ServiceBusClient(_consumerSettings.AzureServiceBus.ConnectionString))
                {
                    // create a sender for the queue
                    ServiceBusSender sender = client.CreateSender(_queueOrTopic);

                    // get the messages to be sent to the service bus queue
                    Queue<ServiceBusMessage> messages = new Queue<ServiceBusMessage>();
                    messages.Enqueue(new ServiceBusMessage(Encoding.UTF8.GetBytes(json)));

                    // total number of messages to be sent to the service bus queue
                    int messageCount = messages.Count;

                    using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

                    if (messageBatch.TryAddMessage(messages.Peek()))
                    {
                        // dequeue the message from the .NET queue once the message is added to the batch.
                        messages.Dequeue();
                    }
                    else
                    {
                        // if the first message can't fit, then it is too large for the batch
                        throw new Exception(
                            $"Message {messageCount - messages.Count} is too large and cannot be sent.");
                    }

                    // now, send the batch
                    await sender.SendMessagesAsync(messageBatch);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
