using BS.ReceiptAnalyzer.Domain.Basic;
using BS.ReceiptAnalyzer.Domain.Model;

namespace BS.ReceiptAnalyzer.Domain.Events
{
    public sealed record AnalysisTaskProgressionNotified(Guid TaskId,
        AnalysisTaskProgression StepType,
        bool Success)
        : DomainEvent;
}
