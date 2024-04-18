using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;

namespace BS.ReceiptAnalyzer.Tool
{
    internal class PathStrategy : ISourceImagePathStrategy, IReceiptRecognitionImagePathStrategy
    {
        public string GetReceiptImagePath(Guid taskId, Guid receiptId)
        {
            throw new NotImplementedException();
        }

        public string GetSourceImagePath(Guid taskId, string extension = "png")
        {
            throw new NotImplementedException();
        }
    }
}
