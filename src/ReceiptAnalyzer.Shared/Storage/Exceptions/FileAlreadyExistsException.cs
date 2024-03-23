namespace BS.ReceiptAnalyzer.Shared.Storage.Exceptions
{
    public class FileAlreadyExistsException : IOException
    {
        public FileAlreadyExistsException(string path)
            : base($"File already exists, path: {path}") { }
    }
}
