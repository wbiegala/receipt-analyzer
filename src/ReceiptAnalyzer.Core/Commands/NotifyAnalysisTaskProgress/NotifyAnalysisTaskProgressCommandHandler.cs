using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Domain.Model;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Commands.NotifyAnalysisTaskProgress
{
    internal class NotifyAnalysisTaskProgressCommandHandler : IRequestHandler<NotifyAnalysisTaskProgressCommand>
    {
        private readonly IAnalysisTaskRepository _repository;
        private readonly ILogger<NotifyAnalysisTaskProgressCommandHandler> _logger;

        public NotifyAnalysisTaskProgressCommandHandler(IAnalysisTaskRepository repository,
            ILogger<NotifyAnalysisTaskProgressCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(NotifyAnalysisTaskProgressCommand command, CancellationToken cancellationToken)
        {
            var task = await _repository.GetById(command.TaskId);

            if (task == null)
                return;

            var step = (AnalysisTaskProgression)command.Step;

            var notification = AnalysisTaskStep.Create(step, command.Success,
                DateTimeOffset.Now, command.StartTime, command.EndTime);
            task.NotifyStepFinished(notification);

            await _repository.UnitOfWork.CommitChangesAsync(cancellationToken);
        }
    }
}
