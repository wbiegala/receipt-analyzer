using Microsoft.Extensions.DependencyInjection;
using BS.ReceiptAnalyzer.Tool;
using BS.ReceiptAnalyzer.Tool.ReceiptRecognition;
using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using BS.ReceiptAnalyzer.Shared;

Console.WriteLine("Starting processing...");

try
{
    var processConfig = args.GetArgs();

    Console.WriteLine($"Processing type: {processConfig.ProcessingType}");
    Console.WriteLine($"Input directory: {processConfig.InputDirectory}");
    Console.WriteLine($"Output directory: {processConfig.OutputDirectory}");

    var services = new ServiceCollection();
    services.AddSingleton(processConfig);
    services.AddReceiptRecognition();
    services.AddLocalStorage($"{processConfig.OutputDirectory}\\temp");

    var provider = services.BuildServiceProvider();

    using var scope = provider.CreateAsyncScope();
    var processor = scope.ServiceProvider.GetRequiredService<IImagesProcessor>();

    await processor.ProcessAsync();
    
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine(ex.ToString());
}
finally
{
    Console.WriteLine("Finished...");
}