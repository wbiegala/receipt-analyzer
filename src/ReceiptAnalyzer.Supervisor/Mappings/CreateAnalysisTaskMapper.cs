using BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask;
using BS.ReceiptAnalyzer.Supervisor.Contract;

namespace BS.ReceiptAnalyzer.Supervisor.Mappings
{
    public static class CreateAnalysisTaskMapper
    {
        public static CreateAnalysisTask.Response MapToResponse(CreateAnalysisTaskCommandResult result)
        {
            return new CreateAnalysisTask.Response
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
