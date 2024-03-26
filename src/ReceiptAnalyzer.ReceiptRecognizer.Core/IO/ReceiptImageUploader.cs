using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO
{
    internal class ReceiptImageUploader : IReceiptImageUploader
    {
        private readonly IStorageService _storageService;
        private readonly IReceiptRecognitionImagePathStrategy _pathStrategy;

        public ReceiptImageUploader(IStorageService storageService,
            IReceiptRecognitionImagePathStrategy pathStrategy)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _pathStrategy = pathStrategy ?? throw new ArgumentNullException(nameof(pathStrategy));
        }

        public async Task UploadReceiptImageAsync(Guid taskId, Guid receiptId, Stream content)
        {
            var path = _pathStrategy.GetReceiptImagePath(taskId, receiptId);
            var result = await _storageService.SaveFileAsync(content, path);

            if (!result.Success)
                throw new IOException(result.Error);
        }
    }
}
