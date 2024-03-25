using BS.ReceiptAnalyzer.ReceiptRecognizer.Core.Model;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core.ML
{
    public interface IReceiptRecognitionService
    {
        Task<IEnumerable<ReceiptRecognized>> FindReceiptsOnImageAsync(Stream source);
    }
}
