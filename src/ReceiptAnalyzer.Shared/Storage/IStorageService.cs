namespace BS.ReceiptAnalyzer.Shared.Storage
{
    public interface IStorageService
    {
        Task<StorageServiceContract.SaveFileResult> SaveFileAsync(Stream file, string path, bool overwrite = false);
        Task<StorageServiceContract.SaveFileResult> SaveFileAsync(byte[] file, string path, bool overwrite = false);
        Task<Stream?> GetFileAsync(string path);
        Task<bool> DeleteFileAsync(string path);
    }
}
