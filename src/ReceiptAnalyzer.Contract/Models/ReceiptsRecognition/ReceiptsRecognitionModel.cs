namespace BS.ReceiptAnalyzer.Contract.Models.ReceiptsRecognition
{
    public sealed class ReceiptsRecognitionModel
    {
        public int ReceiptsRecognized { get; set; }
        public IEnumerable<Guid> Receipts {  get; set; }
    }
}