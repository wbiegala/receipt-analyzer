using BS.ReceiptAnalyzer.Local;
using BS.ReceiptAnalyzer.Local.Core;
using BS.ReceiptAnalyzer.Local.Core.Storage;
using BS.ReceiptAnalyzer.Local.Views;
using BS.ReceiptAnalyzer.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;

namespace ReceiptAnalyzer.Local
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            try
            {
                Application.Run(ServiceProvider.GetRequiredService<MainView>());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "B??d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        private static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddViews();
                services.AddSingleton<IAnalysisTaskManager, SingleAnalysisTaskManager>();
                services.AddSingleton<IStorageFacade, StorageFacade>();
                services.AddLocalReceiptRecognizerCore();
                services.AddLocalStorage(LocalAppConfig.StoragePath);
                services.AddHashing();
            });
        }
    }
}