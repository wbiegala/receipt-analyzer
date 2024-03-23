using Azure.Messaging.ServiceBus;
using BS.ReceiptAnalyzer.Shared.Hashing;
using BS.ReceiptAnalyzer.Shared.ServiceBus;
using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace BS.ReceiptAnalyzer.Shared
{
    public static class SharedDependenciesInstaller
    {
        public static IServiceCollection AddAzureStorage(this IServiceCollection services, 
            string? connectionString, string? containerName = AzureBlobStorageConfiguration.DefaultContainerName)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            services.AddFileSytemPathPolicy();

            services.AddSingleton(_ => new AzureBlobStorageConfiguration { ConnectionString = connectionString, ContainerName = containerName! });
            services.AddScoped<IStorageService, AzureBlobStorageService>();

            return services;
        }

        public static IServiceCollection AddLocalStorage(this IServiceCollection services, string? rootDirectory)
        {
            if (rootDirectory == null)
                throw new ArgumentNullException(nameof(rootDirectory));

            services.AddFileSytemPathPolicy();

            services.AddSingleton(_ => new LocalStorageConfiguration { RootDirectory = rootDirectory });
            services.AddSingleton<IStorageService, LocalStorageService>();

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

        private static IServiceCollection AddFileSytemPathPolicy(this IServiceCollection services)
        {
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

            return services;
        }
    }
}
