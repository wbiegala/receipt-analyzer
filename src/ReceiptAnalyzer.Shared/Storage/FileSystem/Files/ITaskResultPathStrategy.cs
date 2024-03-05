namespace BS.ReceiptAnalyzer.Shared.Storage.FileSystem
{
    public interface ITaskResultPathStrategy
    {
        string GetTaskResultPath(Guid taskId);
    }
}
