namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public interface IReceiptRecognizerService
    {
        Task<ReceiptRecognizerServiceContract.Result> RecognizeReceiptsAsync(Guid taskId);
    }
}
