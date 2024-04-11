using MediatR;

namespace BS.ReceiptAnalyzer.Core.Commands.NotifyAnalysisTaskProgress
{
    public sealed record NotifyAnalysisTaskProgressCommand(
        Guid TaskId,
        int Step,
        bool Success,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime
        ) : IRequest;
}
