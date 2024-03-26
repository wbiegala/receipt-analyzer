using BS.ReceiptAnalyzer.Contract.Models.ReceiptsRecognition;

namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Func.Services
{
    public interface IResultUploader
    {
        Task UploadResultsAsync(Guid taskId, ReceiptsRecognitionModel results);
    }
}
