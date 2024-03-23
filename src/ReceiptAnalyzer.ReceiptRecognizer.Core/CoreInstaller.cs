﻿using Microsoft.Extensions.DependencyInjection;
using BS.ReceiptAnalyzer.Shared;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection AddCore(this IServiceCollection services, string? storageConnectionString)
        {
            services.AddAzureStorage(storageConnectionString);

            services.AddScoped<ISourceImageReceiver, AzureSourceImageReceiver>();
            services.AddScoped<IReceiptRecognizerService, ReceiptRecognizerService>();

            return services;
        }
    }
}