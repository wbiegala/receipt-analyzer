using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace BS.ReceiptAnalyzer.Shared.ServiceBus
{
    public static class ServiceBusSenderExtensions
    {
        public static Task SendMessageAsync<TMessage>(this ServiceBusSender sender,
            TMessage message,
            CancellationToken cancellationToken = default)
                where TMessage : class
        {
            var body = JsonSerializer.Serialize(message);
            var payload = new ServiceBusMessage(body);
            return sender.SendMessageAsync(payload, cancellationToken);
        }
    }
}
