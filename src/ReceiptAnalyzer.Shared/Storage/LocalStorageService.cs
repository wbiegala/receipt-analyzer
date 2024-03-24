using BS.ReceiptAnalyzer.Shared.Storage.Exceptions;

namespace BS.ReceiptAnalyzer.Shared.Storage
{
    internal class LocalStorageService : IStorageService
    {
        private readonly string _rootPath;

        public LocalStorageService(LocalStorageConfiguration storageConfiguration)
        {
            _rootPath = storageConfiguration.RootDirectory ?? "./storage";
        }

        public Task<bool> DeleteFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<StorageServiceContract.GetFileResult> GetFileAsync(string path)
        {
            try
            {
                var fullPath = Path.Combine(_rootPath, path);
                if (!File.Exists(fullPath))
                    throw new FileNotFoundException($"File not found in {fullPath}");

                var result = new StorageServiceContract.GetFileResult(true, File.OpenRead(fullPath));

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new StorageServiceContract.GetFileResult(false, null, ex.Message));
            }
        }

        public async Task<StorageServiceContract.SaveFileResult> SaveFileAsync(Stream file, string path, bool overwrite = false)
        {
            try
            {
                var fullPath = Path.Combine(_rootPath, path);
                if (File.Exists(fullPath))
                {
                    if (overwrite)
                        File.Delete(fullPath);
                    else
                        throw new FileAlreadyExistsException(fullPath);
                }
                
                var directory = Path.GetDirectoryName(fullPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory!);
                
                using var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
                await file.CopyToAsync(fileStream);

                return new StorageServiceContract.SaveFileResult(true);
            }
            catch (Exception ex)
            {
                return new StorageServiceContract.SaveFileResult(false, ex.Message);
            }
        }

        public Task<StorageServiceContract.SaveFileResult> SaveFileAsync(byte[] file, string path, bool overwrite = false)
        {
            return SaveFileAsync(new MemoryStream(file), path, overwrite);
        }
    }
}
