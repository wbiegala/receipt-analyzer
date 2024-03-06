using BS.ReceiptAnalyzer.Domain.Basic;
using MediatR;

namespace BS.ReceiptAnalyzer.Domain.Events
{
    public sealed record AnalysisTaskCreated(Guid TaskId) : DomainEvent, INotification;
}
