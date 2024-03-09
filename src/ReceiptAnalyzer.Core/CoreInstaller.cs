using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BS.ReceiptAnalyzer.Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddStorage(configuration.GetConnectionString("Storage"));
            services.AddServiceBus(configuration.GetConnectionString("ServiceBus"));
            services.AddHashing();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(CoreInstaller));
            });

            return services;
        }
    }
}
