using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using BS.ReceiptAnalyzer.Shared.Storage;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO
{
    internal class AzureSourceImageReceiver : ISourceImageReceiver
    {
        private readonly IStorageService _storageService;
        private readonly ISourceImagePathStrategy _sourceImagePath;

        public AzureSourceImageReceiver(IStorageService storageService, ISourceImagePathStrategy sourceImagePath)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _sourceImagePath = sourceImagePath ?? throw new ArgumentNullException(nameof(sourceImagePath));
        }

        public async Task<Stream> GetSourceImageAsync(Guid taskId)
        {
            var path = _sourceImagePath.GetSourceImagePath(taskId);
            var result = await _storageService.GetFileAsync(path);

            if (!result.Success || result.File == null)
            {
                throw new FileNotFoundException("Source image file not found", path);
            }

            return result.File;
        }
    }
}
