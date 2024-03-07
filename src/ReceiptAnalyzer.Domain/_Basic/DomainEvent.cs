using MediatR;

namespace BS.ReceiptAnalyzer.Domain.Basic
{
    public abstract record DomainEvent : INotification
    {
        public virtual Guid EventId { get; init; } = Guid.NewGuid();
    }
}
