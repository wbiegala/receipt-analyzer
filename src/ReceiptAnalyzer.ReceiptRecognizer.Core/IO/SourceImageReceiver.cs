using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using BS.ReceiptAnalyzer.Shared.Storage;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO
{
    internal class SourceImageReceiver : ISourceImageReceiver
    {
        private readonly IStorageService _storageService;
        private readonly ISourceImagePathStrategy _pathStrategy;

        public SourceImageReceiver(IStorageService storageService, ISourceImagePathStrategy pathStrategy)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _pathStrategy = pathStrategy ?? throw new ArgumentNullException(nameof(pathStrategy));
        }

        public async Task<Stream> GetSourceImageAsync(Guid taskId)
        {
            var path = _pathStrategy.GetSourceImagePath(taskId);
            var result = await _storageService.GetFileAsync(path);

            if (!result.Success || result.File == null)
            {
                throw new FileNotFoundException("Source image file not found", path);
            }

            return result.File;
        }
    }
}
