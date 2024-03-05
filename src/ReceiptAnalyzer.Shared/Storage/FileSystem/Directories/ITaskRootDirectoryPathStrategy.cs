namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    public interface ITaskRootDirectoryPathStrategy
    {
        string GetTaskDirectoryPath(Guid taskId);
    }
}
