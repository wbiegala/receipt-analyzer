namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO
{
    public interface IReceiptImageUploader
    {
        Task UploadReceiptImageAsync(Guid taskId, Guid receiptId, Stream content);
    }
}
