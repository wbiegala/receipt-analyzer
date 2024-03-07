using BS.ReceiptAnalyzer.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Commands.FailAnalysisTask
{
    internal class FailAnalysisTaskCommandHandler : IRequestHandler<FailAnalysisTaskCommand>
    {
        private readonly ReceiptAnalyzerDbContext _dbContext;
        private readonly ILogger<FailAnalysisTaskCommandHandler> _logger;

        public FailAnalysisTaskCommandHandler(ReceiptAnalyzerDbContext dbContext, ILogger<FailAnalysisTaskCommandHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(FailAnalysisTaskCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(FailAnalysisTaskCommandHandler)} - processing commad with id={command.CommandId}.");

            var analysisTask = await _dbContext.Tasks.FindAsync(command.TaskId, cancellationToken);

            if (analysisTask == null)
                return;

            analysisTask.Fail(command.Reason);

            await _dbContext.SaveChangesAndPublishDomainEvents();
        }
    }
}
