using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Domain.Model;
using BS.ReceiptAnalyzer.Shared.Hashing;
using BS.ReceiptAnalyzer.Shared.Storage;
using BS.ReceiptAnalyzer.Shared.Storage.FileSystem;
using BS.ReceiptAnalyzer.Shared.Storage.Images;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask
{
    internal class CreateAnalysisTaskCommandHandler : IRequestHandler<CreateAnalysisTaskCommand, CreateAnalysisTaskCommandResult>
    {
        private readonly ReceiptAnalyzerDbContext _dbContext;
        private readonly IStorageService _storageService;
        private readonly IHashService _hashService;
        private readonly ISourceImagePathStrategy _sourceImagePathStrategy;
        private readonly ILogger _logger;

        public CreateAnalysisTaskCommandHandler(
            ReceiptAnalyzerDbContext dbContext,
            IStorageService storageService,
            IHashService hashService,
            ISourceImagePathStrategy sourceImagePathStrategy,
            ILogger<CreateAnalysisTaskCommandHandler> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _hashService = hashService ?? throw new ArgumentNullException(nameof(hashService));
            _sourceImagePathStrategy = sourceImagePathStrategy
                ?? throw new ArgumentNullException(nameof(sourceImagePathStrategy));
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
                    var processedTask = await GetTaskIdThatProcessSameImage(hash);
                    if (processedTask != null)
                        return CreateAnalysisTaskCommandResult.CreateSuccess(processedTask.Id, processedTask.Status.ToString(),
                            processedTask.CreationTime, processedTask.StartTime, false);
                }

                var analysisTask = AnalysisTask.Create(hash);
                await _dbContext.AddAsync(analysisTask);
                await _dbContext.SaveChangesAsync();

                var saveResultError = await SaveImage(analysisTask.Id, command.MIME, bytes);

                if (string.IsNullOrWhiteSpace(saveResultError))
                    analysisTask.Start();
                else
                    analysisTask.Fail(saveResultError);

                await _dbContext.SaveChangesAsync();

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

        private async Task<string?> SaveImage(Guid taskId, string MIME, byte[] content)
        {
            var converted = ImageFormatConverter.ConvertToPng(MIME, content);
            var path = _sourceImagePathStrategy.GetSourceImagePath(taskId, DefaultImageSourceFileExtension);
            var result = await _storageService.SaveFileAsync(converted, path, true);

            return result.Success ? null : result.Error;
        }

        private async Task<AnalysisTask?> GetTaskIdThatProcessSameImage(string hash)
        {
            return await _dbContext.Tasks
                .Where(at => at.ImageHash != null && at.ImageHash.Equals(hash))
                .OrderByDescending(at => at.EndTime)
                .FirstOrDefaultAsync();
        }

        private const string DefaultImageSourceFileExtension = "png";
    }
}
