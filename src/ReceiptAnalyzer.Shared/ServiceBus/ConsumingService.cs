using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BS.ReceiptAnalyzer.Shared.ServiceBus
{
    internal class ConsumingService : BackgroundService
    {
        private readonly ServiceBusClient _client;
        private readonly IServiceProvider _services;

        private List<ServiceBusProcessor> _processors = new();

        public ConsumingService(IServiceProvider services)
        {
            var config = services.GetRequiredService<ServiceBusConfiguration>();
            _client = new ServiceBusClient(config.ConnectionString);
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scope = _services.CreateAsyncScope();
            var consumers = scope.ServiceProvider.GetServices<IServiceBusConsumer>();

            if (consumers == null || !consumers.Any())
                return;

            foreach (var consumer in consumers)
            {
                var processor = _client.CreateProcessor(consumer.QueueOrSubscription);
                processor.ProcessMessageAsync += consumer.ConsumeAsync;
                processor.ProcessErrorAsync += consumer.HandleErrorAsync;
                _processors.Add(processor);
            }

            await Task.WhenAll(_processors.Select(proc => proc.StartProcessingAsync(stoppingToken)));
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(_processors.Select(proc => proc.StopProcessingAsync(cancellationToken)));
            await base.StopAsync(cancellationToken);
        }
    }
}
