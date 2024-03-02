using BS.ReceiptAnalyzer.Domain.Basic;

namespace BS.ReceiptAnalyzer.Domain.Events
{
    public sealed record AnalysisTaskFinished(Guid TaskId) : DomainEvent;
}
