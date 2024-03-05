namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    public interface IOcrResultPathStrategy
    {
        string GetOcrResultPath(Guid taskId, Guid receiptId);
    }
}
