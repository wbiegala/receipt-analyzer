using MediatR;

namespace BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask
{
    public sealed record CreateAnalysisTaskCommand(bool Force, Stream File, string MIME)
        : CommandBase, IRequest<CreateAnalysisTaskCommandResult>;

}
