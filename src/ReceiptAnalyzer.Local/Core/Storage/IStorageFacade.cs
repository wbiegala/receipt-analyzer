namespace BS.ReceiptAnalyzer.Local.Core.Storage
{
    public interface IStorageFacade
    {
        Task<Tuple<bool, string?>> SaveSourceImage(string sourcePath, Guid taskId);
        string GetSourceImagePath(Guid taskId);
    }
}
