using Microsoft.Extensions.DependencyInjection;

namespace BS.ReceiptAnalyzer.Data
{
    public static class DataInstaller
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAnalysisTaskRepository, AnalysisTaskRepository>();

            return services;
        }
    }
}
