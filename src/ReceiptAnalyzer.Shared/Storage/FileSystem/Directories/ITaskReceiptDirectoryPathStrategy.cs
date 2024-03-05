namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    public interface ITaskReceiptDirectoryPathStrategy
    {
        string GetReceiptDirectoryPath(Guid taskId, Guid receiptId);
    }
}
