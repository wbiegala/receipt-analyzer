namespace BS.ReceiptAnalyzer.Core.Commands
{
    public abstract record CommandBase
    {
        public virtual Guid CommandId { get; init; }
    }
}
