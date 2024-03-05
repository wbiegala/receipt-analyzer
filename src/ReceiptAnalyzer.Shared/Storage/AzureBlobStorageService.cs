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

        public async Task<bool> SaveFileAsync(Stream file, string path, bool overwrite = false)
        {
            try
            {
                var serviceClient = new BlobServiceClient(_connectionString);
                var containerClient = serviceClient.GetBlobContainerClient(_containerName);
                await containerClient.CreateIfNotExistsAsync();
                var blobClient = containerClient.GetBlobClient(path);

                var response = await blobClient.UploadAsync(file, overwrite);

                return true;
            }
            catch (Exception ex)
            {
                return false;
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
