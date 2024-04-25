using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace BS.ReceiptAnalyzer.Shared.ServiceBus
{
    public abstract class ServiceBusConsumer<TMessage> : IServiceBusConsumer
        where TMessage : class
    {
        public abstract string QueueOrSubscription { get; }

        public async Task ConsumeAsync(ProcessMessageEventArgs args)
        {
            var body = args.Message.Body.ToString();

            var message = JsonSerializer.Deserialize<TMessage>(body);
            if (message == null)
                return;

            try
            {
                await ConsumeAsync(message, CancellationToken.None);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                await args.DeadLetterMessageAsync(args.Message, ex.Message);
            }            
        }

        public Task HandleErrorAsync(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }

        public abstract Task ConsumeAsync(TMessage message, 
            CancellationToken cancellationToken);
    }
}
