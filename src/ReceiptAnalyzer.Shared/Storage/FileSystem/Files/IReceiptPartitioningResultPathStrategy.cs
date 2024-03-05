namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    public interface IReceiptPartitioningResultPathStrategy
    {
        string GetReceiptPartitioningResultPath(Guid taskId, Guid receiptId);
    }
}
