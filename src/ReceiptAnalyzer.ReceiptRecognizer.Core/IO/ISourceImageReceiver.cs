namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core.IO
{
    public interface ISourceImageReceiver
    {
        Task<Stream> GetSourceImageAsync(Guid taskId);
    }
}
