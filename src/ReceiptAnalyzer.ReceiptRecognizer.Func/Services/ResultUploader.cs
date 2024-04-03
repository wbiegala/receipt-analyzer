using BS.ReceiptAnalyzer.Contract.Models.ReceiptsRecognition;
using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using System.Text;
using System.Text.Json;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Func.Services
{
    internal class ResultUploader : IResultUploader
    {
        private readonly IStorageService _storageService;
        private readonly IReceiptRecognitionResultPathStrategy _pathStrategy;

        public ResultUploader(IStorageService storageService,
            IReceiptRecognitionResultPathStrategy pathStrategy)
        {
            _storageService = storageService
                ?? throw new ArgumentNullException(nameof(storageService));
            _pathStrategy = pathStrategy
                ?? throw new ArgumentNullException(nameof(pathStrategy));
        }

        public async Task UploadResultsAsync(Guid taskId, ReceiptsRecognitionModel results)
        {
            var path = _pathStrategy.GetReceiptRecognitionResultFilePath(taskId);
            var fileContent = JsonSerializer.Serialize(results);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent));

            await _storageService.SaveFileAsync(stream, path, true);
        }
    }
}
