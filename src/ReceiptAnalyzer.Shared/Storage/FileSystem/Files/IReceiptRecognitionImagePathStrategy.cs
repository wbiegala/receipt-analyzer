namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    public interface IReceiptRecognitionImagePathStrategy
    {
        string GetReceiptImagePath(Guid taskId, Guid receiptId);
    }
}
