using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.ML;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.Model;
using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.Images;
using System.Collections.Concurrent;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    internal class ReceiptRecognizerService : IReceiptRecognizerService
    {
        private readonly ISourceImageReceiver _sourceImageReceiver;
        private readonly IReceiptImageUploader _receiptImageUploader;
        private readonly IReceiptRecognitionService _receiptRecognitionService;

        public ReceiptRecognizerService(ISourceImageReceiver sourceImageReceiver,
            IReceiptImageUploader receiptImageUploader,
            IReceiptRecognitionService receiptRecognitionService)
        {
            _sourceImageReceiver = sourceImageReceiver ?? throw new ArgumentNullException(nameof(sourceImageReceiver));
            _receiptImageUploader = receiptImageUploader ?? throw new ArgumentNullException(nameof(receiptImageUploader));
            _receiptRecognitionService = receiptRecognitionService ?? throw new ArgumentNullException(nameof(receiptRecognitionService));
        }

        public async Task<ReceiptRecognizerServiceContract.Result> RecognizeReceiptsAsync(Guid taskId)
        {
            try
            {
                var source = await _sourceImageReceiver.GetSourceImageAsync(taskId);
                var sourceBytes = FileHelper.GetBytes(source);
                var recognitionResult = await _receiptRecognitionService.FindReceiptsOnImageAsync(new MemoryStream(sourceBytes));

                if (recognitionResult is null || !recognitionResult.Any())
                    return FailResult(ReceiptsNotFound);

                var receiptsIds = await ProcessReceiptsAsync(taskId, recognitionResult, sourceBytes);

                return SuccessResult(receiptsIds);
            }
            catch (Exception ex)
            {
                return FailResult(ex.Message);
            }

        }

        private async Task<IEnumerable<Guid>> ProcessReceiptsAsync(Guid taskId,
            IEnumerable<ReceiptRecognized> recognized,
            byte[] source)
        {
            var result = new ConcurrentBag<Guid>();

            await Parallel.ForEachAsync(recognized, async (receipt, ct) =>
            {
                var id = await ProcessReceiptAsync(taskId, receipt, source);
                result.Add(id);
            });

            return result.ToList();
        }

        private async Task<Guid> ProcessReceiptAsync(Guid taskId, ReceiptRecognized receipt, byte[] source)
        {
            var id = CreateReceiptId();
            var receiptImage = ImageCroppingHelper.CropImage(source, receipt.X1, receipt.Y1, receipt.X2, receipt.Y2);
            await _receiptImageUploader.UploadReceiptImageAsync(taskId,id, new MemoryStream(receiptImage));

            return id;
        }

        private static ReceiptRecognizerServiceContract.Result FailResult(string reason) =>
            new ReceiptRecognizerServiceContract.Result
            {
                IsSuccess = false,
                FinishedAt = DateTimeOffset.Now,
                FailReason = reason,
            };

        private static ReceiptRecognizerServiceContract.Result SuccessResult(IEnumerable<Guid> receips) =>
            new ReceiptRecognizerServiceContract.Result
            {
                IsSuccess = true,
                FinishedAt = DateTimeOffset.Now,
                ReceiptsFound = receips.Count(),
                Receipts = receips
            };

        private static Guid CreateReceiptId() => Guid.NewGuid();

        private const string ReceiptsNotFound = "No receipt was recognized";
    }
}
