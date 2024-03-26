using Microsoft.Extensions.DependencyInjection;
using BS.ReceiptAnalyzer.Shared;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.ML;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection AddAzureReceiptRecognizerCore(this IServiceCollection services, string? storageConnectionString)
        {
            services.AddAzureStorage(storageConnectionString);

            services.AddSingleton<IReceiptRecognitionService, ReceiptRecognitionService>();
            services.AddScoped<ISourceImageReceiver, SourceImageReceiver>();
            services.AddScoped<IReceiptImageUploader, ReceiptImageUploader>();
            services.AddScoped<IReceiptRecognizerService, ReceiptRecognizerService>();

            return services;
        }

        public static IServiceCollection AddLocalReceiptRecognizerCore(this IServiceCollection services)
        {
            services.AddSingleton<IReceiptRecognitionService, ReceiptRecognitionService>();
            services.AddSingleton<ISourceImageReceiver, SourceImageReceiver>();
            services.AddSingleton<IReceiptImageUploader, ReceiptImageUploader>();
            services.AddSingleton<IReceiptRecognizerService, ReceiptRecognizerService>();

            return services;
        }

    }
}