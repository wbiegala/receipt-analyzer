namespace BS.ReceiptAnalyzer.ReceiptPartitioner.Core.IO
{
    public interface IReceiptPartUploader
    {
        Task UploadHeaderAsync(Guid taskId, Guid receiptId, Guid partId, Stream content);
    }
}
