using BS.ReceiptAnalyzer.Domain.Basic;
using MediatR;

namespace BS.ReceiptAnalyzer.Domain.Events
{
    public sealed record AnalysisTaskStarted(Guid TaskId) : DomainEvent, INotification;
}
