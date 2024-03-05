namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    public interface IReceiptPartitioningImagePathStrategy
    {
        string GetReceiptPartPath(Guid taskId, Guid receiptId, string partType, int partNumber);
    }
}
