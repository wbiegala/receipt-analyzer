namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public class ReceiptRecognizerServiceContract
    {
        public sealed record Result
        {
            public DateTimeOffset FinishedAt { get; init; }
            public bool IsSuccess { get; set; }
            public string? FailReason { get; set; }
        }
    }
}
