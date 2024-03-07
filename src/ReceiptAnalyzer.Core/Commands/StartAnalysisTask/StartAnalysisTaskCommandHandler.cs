using BS.ReceiptAnalyzer.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Commands.StartAnalysisTask
{
    internal class StartAnalysisTaskCommandHandler : IRequestHandler<StartAnalysisTaskCommand>
    {
        private readonly ReceiptAnalyzerDbContext _dbContext;
        private readonly ILogger<StartAnalysisTaskCommandHandler> _logger;

        public StartAnalysisTaskCommandHandler(ReceiptAnalyzerDbContext dbContext, ILogger<StartAnalysisTaskCommandHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(StartAnalysisTaskCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(StartAnalysisTaskCommandHandler)} - processing commad with id={command.CommandId}.");

            var analysisTask = await _dbContext.Tasks.FindAsync(command.TaskId, cancellationToken);

            if (analysisTask == null)
                return;

            analysisTask.Start();

            await _dbContext.SaveChangesAndPublishDomainEvents();
        }
    }
}
