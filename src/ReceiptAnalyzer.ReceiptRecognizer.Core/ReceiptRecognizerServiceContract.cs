namespace BS.ReceiptAnalyzer.ReceiptRecognizer.Core
{
    public class ReceiptRecognizerServiceContract
    {
        public sealed record Result
        {
            public DateTimeOffset FinishedAt { get; init; }
            public bool IsSuccess { get; init; }
            public string? FailReason { get; init; }
            public int ReceiptsFound { get; init; }
            public IEnumerable<Guid> Receipts { get; init; } = Enumerable.Empty<Guid>();
        }
    }
}
