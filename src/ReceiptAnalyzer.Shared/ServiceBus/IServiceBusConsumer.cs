using Azure.Messaging.ServiceBus;

namespace BS.ReceiptAnalyzer.Shared.ServiceBus
{
    public interface IServiceBusConsumer
    {
        string QueueOrSubscription { get; }
        Task ConsumeAsync(ProcessMessageEventArgs args);
        Task HandleErrorAsync(ProcessErrorEventArgs args);
    }
}
