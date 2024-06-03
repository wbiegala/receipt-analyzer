namespace BS.ReceiptAnalyzer.ReceiptPartitioner.Core.IO
{
    public interface IReceiptReceiver
    {
        Task<Stream> GetReceiptImageAsync(Guid taskId, Guid receiptId);
    }
}
