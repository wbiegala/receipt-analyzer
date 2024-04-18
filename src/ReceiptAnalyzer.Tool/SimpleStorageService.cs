using BS.ReceiptAnalyzer.Shared.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.ReceiptAnalyzer.Tool
{
    internal class SimpleStorageService : IStorageService
    {
        public Task<bool> DeleteFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<StorageServiceContract.GetFileResult> GetFileAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<StorageServiceContract.SaveFileResult> SaveFileAsync(Stream file, string path, bool overwrite = false)
        {
            throw new NotImplementedException();
        }

        public Task<StorageServiceContract.SaveFileResult> SaveFileAsync(byte[] file, string path, bool overwrite = false)
        {
            throw new NotImplementedException();
        }
    }
}
