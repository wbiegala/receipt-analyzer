using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;
using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    internal class ReceiptRecognizerService : IReceiptRecognizerService
    {
        private readonly ISourceImageReceiver _sourceImageReceiver;

        public ReceiptRecognizerService(ISourceImageReceiver sourceImageReceiver)
        {
            _sourceImageReceiver = sourceImageReceiver ?? throw new ArgumentNullException(nameof(sourceImageReceiver));
        }

        public async Task<ReceiptRecognizerServiceContract.Result> RecognizeReceiptsAsync(Guid taskId)
        {
            var source = await _sourceImageReceiver.GetSourceImageAsync(taskId);

            return new ReceiptRecognizerServiceContract.Result
            {
                FinishedAt = DateTimeOffset.Now,
                IsSuccess = true
            };
        }
    }
}
