using BS.ReceiptAnalyzer.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Commands.FailAnalysisTask
{
    internal class FailAnalysisTaskCommandHandler : IRequestHandler<FailAnalysisTaskCommand>
    {
        private readonly IAnalysisTaskRepository _repository;
        private readonly ILogger<FailAnalysisTaskCommandHandler> _logger;

        public FailAnalysisTaskCommandHandler(IAnalysisTaskRepository repository, ILogger<FailAnalysisTaskCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(FailAnalysisTaskCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(FailAnalysisTaskCommandHandler)} - processing commad with id={command.CommandId}.");

            var analysisTask = await _repository.GetById(command.TaskId);

            if (analysisTask == null)
                return;

            analysisTask.Fail(command.Reason);

            await _repository.UnitOfWork.CommitChangesAsync(cancellationToken);
        }
    }
}
