using Microsoft.Extensions.DependencyInjection;

namespace BS.ReceiptAnalyzer.Shared.ServiceBus
{
    public static class ServiceBusConsumerInstaller
    {
        public static IServiceCollection AddServiceBusConsumer<TConsumer>(this IServiceCollection services)
            where TConsumer : class, IServiceBusConsumer
        {
            services.AddSingleton<IServiceBusConsumer, TConsumer>();
            
            return services;
        }
    }
}
