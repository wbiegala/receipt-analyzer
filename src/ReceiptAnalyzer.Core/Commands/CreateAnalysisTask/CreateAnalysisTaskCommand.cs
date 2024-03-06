using MediatR;

namespace BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask
{
    public sealed record CreateAnalysisTaskCommand : CommandBase, IRequest<CreateAnalysisTaskCommandResult>
    {
        public bool Force { get; init; }
        public Stream File { get; init; }
        public string MIME { get; init; }
    }
}
