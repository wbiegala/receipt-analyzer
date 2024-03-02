using BS.ReceiptAnalyzer.Domain.Basic;

namespace BS.ReceiptAnalyzer.Domain.Events
{
    public sealed record AnalysisTaskCanceled(Guid TaskId) : DomainEvent;
}
