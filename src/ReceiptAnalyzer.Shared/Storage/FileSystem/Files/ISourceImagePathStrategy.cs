namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem 
{ 
    public interface ISourceImagePathStrategy
    {
        string GetSourceImagePath(Guid taskId, string extension = "png");
    }
}
