using BS.ReceiptAnalyzer.ReceiptRecognizer.Core;

namespace BS.ReceiptAnalyzer.Tool.ReceiptRecognition
{
    internal class ReceiptRecognitionProcessor : IImagesProcessor
    {
        private readonly IReceiptRecognizerService _recognizer;

        public ReceiptRecognitionProcessor(IReceiptRecognizerService recognizer)
        {
            _recognizer = recognizer ?? throw new ArgumentNullException(nameof(recognizer));
        }

        public async Task ProcessAsync()
        {
            throw new NotImplementedException();
        }
    }
}
