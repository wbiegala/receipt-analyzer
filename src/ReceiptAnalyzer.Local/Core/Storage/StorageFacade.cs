using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using ImageFormatHelper = BS.ReceiptAnalyzer.Shared.Storage.Images.ImageFormatHelper;

namespace BS.ReceiptAnalyzer.Local.Core.Storage
{
    internal class StorageFacade : IStorageFacade
    {
        private readonly IStorageService _storageService;
        private readonly ISourceImagePathStrategy _sourceImagePathStrategy;

        public StorageFacade(IStorageService storageService,
            ISourceImagePathStrategy sourceImagePathStrategy)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _sourceImagePathStrategy = sourceImagePathStrategy ?? throw new ArgumentNullException(nameof(sourceImagePathStrategy));
        }

        public async Task<Tuple<bool, string?>> SaveSourceImage(string sourcePath, Guid taskId)
        {
            var info = new FileInfo(sourcePath);
            var mime = ImageFormatHelper.GetMimeFileFormant(info.FullName);
            var orginal = File.ReadAllBytes(info.FullName);
            var content = ImageFormatHelper.ConvertToPng(mime, orginal);
            var path = _sourceImagePathStrategy.GetSourceImagePath(taskId);
            var saveResult = await _storageService.SaveFileAsync(content, path, true);

            return new Tuple<bool, string?>(saveResult.Success, saveResult.Error);
        }

        public string GetSourceImagePath(Guid taskId)
        {
            return Path.Combine(LocalAppConfig.StoragePath, _sourceImagePathStrategy.GetSourceImagePath(taskId));
        }

    }
}
