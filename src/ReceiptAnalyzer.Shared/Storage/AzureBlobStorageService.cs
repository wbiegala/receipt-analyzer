using Azure.Storage.Blobs;

namespace BS.ReceiptAnalyzer.Shared.Storage
{
    internal class AzureBlobStorageService : IStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public AzureBlobStorageService(AzureBlobStorageConfiguration configuration)
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
                var blobClient = await GetClientAsync(path);

                var response = await blobClient.UploadAsync(file, overwrite);

                return new StorageServiceContract.SaveFileResult(true);
            }
            catch (Exception ex)
            {
                return new StorageServiceContract.SaveFileResult(false,
                    $"{ex.GetType().Name} - {ex.Message}");
            }
        }

        public Task<bool> DeleteFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<StorageServiceContract.GetFileResult> GetFileAsync(string path)
        {
            try
            {
                var blobClient = await GetClientAsync(path);
                var result = new MemoryStream();
                var response = await blobClient.DownloadToAsync(result);

                result.Position = 0;
                return new StorageServiceContract.GetFileResult(true, result);
            }
            catch (Exception ex)
            {
                return new StorageServiceContract.GetFileResult(false, null, ex.Message);
            }
        }


        private async Task<BlobClient> GetClientAsync(string path)
        {
            var serviceClient = new BlobServiceClient(_connectionString);
            var containerClient = serviceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();

            return containerClient.GetBlobClient(path);
        }
    }
}
