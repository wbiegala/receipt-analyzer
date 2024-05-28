using Microsoft.Extensions.DependencyInjection;
using BS.ReceiptAnalyzer.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.ReceiptAnalyzer.ReceiptPartitioner.Core
{
    public static class CoreInstaller
    {
        public static IServiceCollection AddAzureReceiptPartitioning(this IServiceCollection services, string? storageConnectionString)
        {
            services.AddAzureStorage(storageConnectionString);


            return services;
        }
    }
}
