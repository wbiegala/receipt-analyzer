namespace BS.ReceiptAnalyzer.Domain.Basic
{
    public abstract record DomainEvent
    {
        public virtual Guid EventId { get; init; } = Guid.NewGuid();
    }
}
