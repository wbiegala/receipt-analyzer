using BS.ReceiptAnalyzer.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Commands.StartAnalysisTask
{
    internal class StartAnalysisTaskCommandHandler : IRequestHandler<StartAnalysisTaskCommand>
    {
        private readonly IAnalysisTaskRepository _repository;
        private readonly ILogger<StartAnalysisTaskCommandHandler> _logger;

        public StartAnalysisTaskCommandHandler(IAnalysisTaskRepository repository, ILogger<StartAnalysisTaskCommandHandler> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(StartAnalysisTaskCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(StartAnalysisTaskCommandHandler)} - processing commad with id={command.CommandId}.");

            var analysisTask = await _repository.GetById(command.TaskId);

            if (analysisTask == null)
                return;

            analysisTask.Start();

            await _repository.UnitOfWork.CommitChangesAsync(cancellationToken);
        }
    }
}
