using Microsoft.Extensions.DependencyInjection;
using BS.ReceiptAnalyzer.Shared;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO;

namespace ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services, string? storageConnectionString)
        {
            services.AddStorage(storageConnectionString);

            services.AddScoped<ISourceImageReceiver, SourceImageReceiver>();
            services.AddScoped<IReceiptRecognizerService, ReceiptRecognizerService>();

            return services;
        }
    }
}