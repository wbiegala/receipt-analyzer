namespace BS.ReceiptAnalyzer.Core.Commands
{
    public record CommandResultBase
    {
        public virtual bool Success { get; init; }
        public virtual string? FailReason {  get; init; }
    }
}
