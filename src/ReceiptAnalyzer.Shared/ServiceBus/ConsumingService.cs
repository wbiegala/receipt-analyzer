using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;

namespace BS.ReceiptAnalyzer.Shared.ServiceBus
{
    internal class ConsumingService : BackgroundService
    {
        private readonly ServiceBusClient _client;
        private readonly IEnumerable<IServiceBusConsumer> _consumers;

        private List<ServiceBusProcessor> _processors = new();

        public ConsumingService(ServiceBusConfiguration config, IEnumerable<IServiceBusConsumer> consumers)
        {
            _client = new ServiceBusClient(config.ConnectionString);
            _consumers = consumers;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_consumers == null || !_consumers.Any())
                return Task.CompletedTask;

            foreach (var consumer in _consumers)
            {
                var processor = _client.CreateProcessor(consumer.QueueOrSubscription);
                processor.ProcessMessageAsync += consumer.ConsumeAsync;
                processor.ProcessErrorAsync += consumer.HandleErrorAsync;
                _processors.Add(processor);
            }

            return Task.WhenAll(_processors.Select(proc => proc.StartProcessingAsync(stoppingToken)));
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(_processors.Select(proc => proc.StopProcessingAsync(cancellationToken)));
            await base.StopAsync(cancellationToken);
        }
    }
}
