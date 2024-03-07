using MediatR;

namespace BS.ReceiptAnalyzer.Core.Commands.FailAnalysisTask
{
    public sealed record FailAnalysisTaskCommand(Guid TaskId, string Reason) : CommandBase, IRequest;
}
