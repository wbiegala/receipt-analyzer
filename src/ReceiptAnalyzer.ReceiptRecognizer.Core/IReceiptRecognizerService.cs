using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;

namespace ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public interface IReceiptRecognizerService
    {
        Task<ReceiptRecognizerServiceContract.Result> RecognizeReceiptsAsync(Guid taskId);
    }
}
