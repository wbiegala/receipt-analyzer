using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Func.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((ctx, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddAzureReceiptRecognizerCore(ctx.Configuration.GetConnectionString("Storage"));
        services.AddScoped<IResultUploader, ResultUploader>();
    }).Build();

host.Run();
