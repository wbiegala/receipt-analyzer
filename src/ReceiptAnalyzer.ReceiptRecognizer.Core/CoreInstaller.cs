using Microsoft.Extensions.DependencyInjection;

namespace ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services, string? storageConnectionString)
        {
            services.AddScoped<IReceiptRecognizerService, ReceiptRecognizerService>();

            return services;
        }
    }
}
