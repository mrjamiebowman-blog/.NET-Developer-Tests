using Microsoft.Extensions.Logging;
using System.Text;

namespace MrJB.DeveloperTests.App.Tests.Common.Helpers
{
    public class MockLogger<T> : ILogger<T>
    {
        public StringBuilder Messages = new StringBuilder();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception ex, Func<TState, Exception, string> formatter)
        {
            // overriding
            var messages = formatter.Invoke(state, ex);
            Messages.Append(messages);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // overriding
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
