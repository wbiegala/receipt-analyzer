using Microsoft.Extensions.DependencyInjection;

namespace BS.ReceiptAnalyzer.Tool.ReceiptRecognition
{
    internal static class ReceiptRecognitionDeps
    {
        public static IServiceCollection AddReceiptRecognition(this IServiceCollection services,
            string inputDir, string outputDir)
        {
            services.AddSingleton<IImagesProcessor, ReceiptRecognitionProcessor>();

            return services;
        }
    }
}
