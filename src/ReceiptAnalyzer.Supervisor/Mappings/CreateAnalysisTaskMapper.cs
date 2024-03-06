using BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask;
using BS.ReceiptAnalyzer.Supervisor.Contract;

namespace BS.ReceiptAnalyzer.Supervisor.Mappings
{
    public static class CreateAnalysisTaskMapper
    {
        public static CreateAnalysisTask.CreateAnalysisTaskResponse MapToResponse(CreateAnalysisTaskCommandResult result)
        {
            return new CreateAnalysisTask.CreateAnalysisTaskResponse
            {
                TaskId = result.TaskId,
                Status = result.Status,
                CreatedAt = result.CreatedAt,
                StartedAt = result.StartedAt,
                IsNew = result.IsNew
            };
        }
    }
}
