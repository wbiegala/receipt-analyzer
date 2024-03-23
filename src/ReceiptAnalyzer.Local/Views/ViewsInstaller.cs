using Microsoft.Extensions.DependencyInjection;

namespace BS.ReceiptAnalyzer.Local.Views
{
    internal static class ViewsInstaller
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddTransient<MainView>();

            return services;
        }
    }
}
