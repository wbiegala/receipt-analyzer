using Contract = BS.ReceiptAnalyzer.Local.Core.TaskManagerContract;

namespace BS.ReceiptAnalyzer.Local.Core
{
    public interface IAnalysisTaskManager
    {

        Task<Contract.CreateTask.Result> CreateTask(Contract.CreateTask.Request request);
        Task<Contract.ExecuteNextStep.Result> ExecuteNextStep(Contract.ExecuteNextStep.Request request);

        string GetSourceImagePath(Guid taskId);
    }
}

