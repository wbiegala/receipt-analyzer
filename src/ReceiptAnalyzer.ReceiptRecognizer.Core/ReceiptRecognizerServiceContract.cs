namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public class ReceiptRecognizerServiceContract
    {
        public sealed record Result
        {
            public Guid TaskId { get; init; }
            public DateTimeOffset StartTime { get; init; }
            public DateTimeOffset EndTime { get; init; }
            public bool IsSuccess { get; set; }
            public string? FailReason { get; set; }
        }
    }
}
