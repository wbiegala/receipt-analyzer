using Azure.Storage.Blobs;

namespace BS.ReceiptAnalyzer.Shared.Storage
{
    internal class AzureBlobStorageService : IStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public AzureBlobStorageService(StorageConfiguration configuration)
        {
            _connectionString = configuration.ConnectionString;
            _containerName = configuration.ContainerName;
        }

        public Task<StorageServiceContract.SaveFileResult> SaveFileAsync(byte[] file, string path, bool overwrite = false)
        {
            return SaveFileAsync(new MemoryStream(file), path, overwrite);
        }

        public async Task<StorageServiceContract.SaveFileResult> SaveFileAsync(Stream file, string path, bool overwrite = false)
        {
            try
            {
                var serviceClient = new BlobServiceClient(_connectionString);
                var containerClient = serviceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync();
                var blobClient = containerClient.GetBlobClient(path);

                var response = await blobClient.UploadAsync(file, overwrite);

                return new StorageServiceContract.SaveFileResult(true);
            }
            catch (Exception ex)
            {
                return new StorageServiceContract.SaveFileResult(false, $"{ex.GetType().Name} - {ex.Message}");
            }
        }

        public Task<bool> DeleteFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<Stream?> GetFileAsync(string path)
        {
            throw new NotImplementedException();
        }
    }
}
