using MediatR;

namespace BS.ReceiptAnalyzer.Core.Commands.NotifyAnalysisTaskProgress
{
    internal class NotifyAnalysisTaskProgressCommandHandler : IRequestHandler<NotifyAnalysisTaskProgressCommand>
    {
        public Task Handle(NotifyAnalysisTaskProgressCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
