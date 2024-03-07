using BS.ReceiptAnalyzer.Core.Commands.FailAnalysisTask;
using BS.ReceiptAnalyzer.Core.Commands.StartAnalysisTask;
using BS.ReceiptAnalyzer.Core.Commands.UploadSourceImage;
using BS.ReceiptAnalyzer.Core.Internal.Queries;
using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Domain.Model;
using BS.ReceiptAnalyzer.Shared.Hashing;
using BS.ReceiptAnalyzer.Shared.Storage;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask
{
    internal class CreateAnalysisTaskCommandHandler : IRequestHandler<CreateAnalysisTaskCommand, CreateAnalysisTaskCommandResult>
    {
        private readonly ReceiptAnalyzerDbContext _dbContext;
        private readonly IHashService _hashService;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateAnalysisTaskCommandHandler> _logger;

        public CreateAnalysisTaskCommandHandler(
            ReceiptAnalyzerDbContext dbContext,
            IHashService hashService,
            IMediator mediator,
            ILogger<CreateAnalysisTaskCommandHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CreateAnalysisTaskCommandResult> Handle(CreateAnalysisTaskCommand command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"{nameof(CreateAnalysisTaskCommandHandler)} - start processing commad with id={command.CommandId}.");
            
            try
            {
                var bytes = FileHelper.GetBytes(command.File);
                var hash = _hashService.GetHash(bytes);

                if (!command.Force)
                {
                    var processedTask = await _mediator.Send(new GetAnalysisTaskByImageHashQuery(hash));
                    if (processedTask != null)
                        return CreateAnalysisTaskCommandResult.CreateSuccess(processedTask.Id, processedTask.Status.ToString(),
                            processedTask.CreationTime, processedTask.StartTime, false);
                }

                var analysisTask = AnalysisTask.Create(hash);
                await _dbContext.AddAsync(analysisTask);
                await _dbContext.SaveChangesAndPublishDomainEvents();

                var saveResult = await _mediator.Send(new UploadSourceImageCommand(analysisTask.Id, command.MIME, bytes));

                await StartOrFailAnalysisTask(analysisTask.Id, saveResult.Success, saveResult.FailReason);

                _logger.LogDebug($"{nameof(CreateAnalysisTaskCommandHandler)} - processing commad with id={command.CommandId} finished.");
                return CreateAnalysisTaskCommandResult.CreateSuccess(analysisTask.Id, analysisTask.Status.ToString(),
                    analysisTask.CreationTime, analysisTask.StartTime, true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private Task StartOrFailAnalysisTask(Guid taskId, bool isImageSaved, string? imageSaveError)
        {
            var command = isImageSaved
                ? _mediator.Send(new StartAnalysisTaskCommand(taskId))
                : _mediator.Send(new FailAnalysisTaskCommand(taskId, imageSaveError!));

            return command;
        }
    }
}
