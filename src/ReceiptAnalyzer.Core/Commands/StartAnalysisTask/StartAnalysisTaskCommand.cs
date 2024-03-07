using MediatR;

namespace BS.ReceiptAnalyzer.Core.Commands.StartAnalysisTask
{
    public sealed record StartAnalysisTaskCommand(Guid TaskId) : CommandBase, IRequest;
}
