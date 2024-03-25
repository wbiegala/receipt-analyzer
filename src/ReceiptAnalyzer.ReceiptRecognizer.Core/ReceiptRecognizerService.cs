using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.ML;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    internal class ReceiptRecognizerService : IReceiptRecognizerService
    {
        private readonly ISourceImageReceiver _sourceImageReceiver;
        private readonly IReceiptRecognitionService _receiptRecognitionService;

        public ReceiptRecognizerService(ISourceImageReceiver sourceImageReceiver,
            IReceiptRecognitionService receiptRecognitionService)
        {
            _sourceImageReceiver = sourceImageReceiver ?? throw new ArgumentNullException(nameof(sourceImageReceiver));
            _receiptRecognitionService = receiptRecognitionService ?? throw new ArgumentNullException(nameof(receiptRecognitionService));
        }

        public async Task<ReceiptRecognizerServiceContract.Result> RecognizeReceiptsAsync(Guid taskId)
        {
            var source = await _sourceImageReceiver.GetSourceImageAsync(taskId);
            var recognitionResult = await _receiptRecognitionService.FindReceiptsOnImageAsync(source);

            return new ReceiptRecognizerServiceContract.Result
            {
                FinishedAt = DateTimeOffset.Now,
                IsSuccess = true
            };
        }
    }
}
