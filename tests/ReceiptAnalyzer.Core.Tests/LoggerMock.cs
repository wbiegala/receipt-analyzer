using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Tests
{
    internal class LoggerMock<TService> : ILogger<TService>
        where TService : class
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            
        }
    }
}
