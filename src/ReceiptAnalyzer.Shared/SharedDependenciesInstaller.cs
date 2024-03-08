﻿using Azure.Messaging.ServiceBus;
using BS.ReceiptAnalyzer.Shared.Hashing;
using BS.ReceiptAnalyzer.Shared.ServiceBus;
using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Serialization;

namespace BS.ReceiptAnalyzer.Shared
{
    public static class SharedDependenciesInstaller
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, 
            string? connectionString, string? containerName = StorageConfiguration.DefaultContainerName)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            services.AddSingleton<FileSytemPathPolicy>();
            services.AddSingleton<ITaskRootDirectoryPathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<ITaskReceiptDirectoryPathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<ISourceImagePathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<IReceiptRecognitionImagePathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<IReceiptRecognitionResultPathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<IReceiptPartitioningImagePathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<IReceiptPartitioningResultPathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<IOcrResultPathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());
            services.AddSingleton<ITaskResultPathStrategy>(ctx => ctx.GetRequiredService<FileSytemPathPolicy>());

            services.AddSingleton(_ => new StorageConfiguration { ConnectionString = connectionString, ContainerName = containerName! });
            services.AddScoped<IStorageService, AzureBlobStorageService>();

            return services;
        }

        public static IServiceCollection AddHashing(this IServiceCollection services)
        {
            services.AddSingleton<IHashService, Sha512HashService>();

            return services;
        }

        public static IServiceCollection AddServiceBus(this IServiceCollection services, string? connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            services.AddSingleton(_ => new ServiceBusConfiguration { ConnectionString = connectionString });
            services.AddSingleton(_ => new ServiceBusClient(connectionString));
            services.AddHostedService<ConsumingService>();

            return services;
        }
    }
}
