using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;

namespace BS.ReceiptAnalyzer.Tool.ReceiptRecognition
{
    internal class ReceiptRecognitionProcessor : IImagesProcessor
    {
        private readonly IReceiptRecognizerService _recognizer;
        private readonly string _inputDirectory;

        public ReceiptRecognitionProcessor(IReceiptRecognizerService recognizer, ProgramArgs config)
        {
            _recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
            _inputDirectory = config.InputDirectory;
        }

        public async Task ProcessAsync()
        {
            var files = GetFiles();

            foreach (var file in files)
            {
                //TODO: processing
            }
        }



        private IEnumerable<string> GetFiles()
        {
            var result = new List<string>();
            result.AddRange(Directory.GetFiles(_inputDirectory, "*.png"));
            result.AddRange(Directory.GetFiles(_inputDirectory, "*.jpg"));
            result.AddRange(Directory.GetFiles(_inputDirectory, "*.jpeg"));

            return result;
        }
    }
}
