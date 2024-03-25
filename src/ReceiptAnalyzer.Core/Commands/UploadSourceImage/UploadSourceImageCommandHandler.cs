using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using BS.ReceiptAnalyzer.Shared.Storage;
using MediatR;
using Microsoft.Extensions.Logging;
using BS.ReceiptAnalyzer.Shared.Storage.Images;

namespace BS.ReceiptAnalyzer.Core.Commands.UploadSourceImage
{
    internal class UploadSourceImageCommandHandler : IRequestHandler<UploadSourceImageCommand, UploadSourceImageCommandResult>
    {
        private const string DefaultImageSourceFileExtension = "png";
        private readonly IStorageService _storageService;
        private readonly ISourceImagePathStrategy _sourceImagePathStrategy;
        private readonly ILogger<UploadSourceImageCommandHandler> _logger;

        public UploadSourceImageCommandHandler(IStorageService storageService, 
            ISourceImagePathStrategy sourceImagePathStrategy,
            ILogger<UploadSourceImageCommandHandler> logger)
        {
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _sourceImagePathStrategy = sourceImagePathStrategy
                ?? throw new ArgumentNullException(nameof(sourceImagePathStrategy));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UploadSourceImageCommandResult> Handle(UploadSourceImageCommand command, CancellationToken cancellationToken)
        {
            var converted = ImageFormatHelper.ConvertToPng(command.MIME, command.Image);
            var path = _sourceImagePathStrategy.GetSourceImagePath(command.TaskId, DefaultImageSourceFileExtension);
            var result = await _storageService.SaveFileAsync(converted, path, true);

            return result.Success
                ? new UploadSourceImageCommandResult { Success = true }
                : new UploadSourceImageCommandResult { Success = false, FailReason = result.Error };
        }
    }
}
