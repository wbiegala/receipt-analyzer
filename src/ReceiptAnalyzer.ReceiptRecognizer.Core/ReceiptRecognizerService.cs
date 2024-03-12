using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;

namespace ReceiptAnalyzer.ReceiptRecognizer.Core
{
    internal class ReceiptRecognizerService : IReceiptRecognizerService
    {
        public Task<ReceiptRecognizerServiceContract.Result> RecognizeReceiptsAsync(Guid taskId)
        {
            return Task.FromResult(new ReceiptRecognizerServiceContract.Result
            {
                TaskId = taskId,
                StartTime = DateTimeOffset.Now,
                EndTime = DateTimeOffset.Now,
                IsSuccess = true,
                FailReason = null,
            });
        }
    }
}
