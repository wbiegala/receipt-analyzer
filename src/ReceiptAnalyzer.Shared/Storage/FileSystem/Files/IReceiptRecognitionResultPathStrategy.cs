namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem 
{ 
    public interface IReceiptRecognitionResultPathStrategy
    {
        string GetReceiptRecognitionResultFilePath(Guid taskId);
    }
}
