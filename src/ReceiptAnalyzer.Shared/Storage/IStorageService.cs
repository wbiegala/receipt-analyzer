namespace BS.ReceiptAnalyzer.Shared.Storage
{
    public interface IStorageService
    {
        Task<bool> SaveFileAsync(Stream file, string path, bool overwrite = false);
        Task<Stream?> GetFileAsync(string path);
        Task<bool> DeleteFileAsync(string path);
    }
}
