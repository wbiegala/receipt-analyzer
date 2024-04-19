using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;

namespace BS.ReceiptAnalyzer.Tool
{
    internal class PathStrategy : ISourceImagePathStrategy, IReceiptRecognitionImagePathStrategy
    {
        private readonly string _inputRoot;
        private readonly string _outputRoot;

        public PathStrategy(ProgramArgs config)
        {
            _inputRoot = config.InputDirectory;
            _outputRoot = config.OutputDirectory;
        }

        public string GetSourceImagePath(Guid taskId, string extension = "png")
        {
            throw new NotImplementedException();
        }

        public string GetReceiptImagePath(Guid taskId, Guid receiptId)
        {
            throw new NotImplementedException();
        }
    }
}
