namespace BS.ReceiptAnalyzer.ReceiptPartitioner.Core
{
    public interface IReceiptPartitionerService
    {
        Task<ReceiptPartitionerServiceContract.Result> PartitionReceiptsAsync(Guid taskId);
    }
}
