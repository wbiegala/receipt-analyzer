using BS.ReceiptAnalyzer.Domain.Basic;
using MediatR;

namespace BS.ReceiptAnalyzer.Domain.Events
{
    public sealed record AnalysisTaskFailed(Guid TaskId): DomainEvent, INotification;
}
