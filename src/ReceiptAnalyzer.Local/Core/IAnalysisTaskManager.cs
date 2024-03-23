using BS.ReceiptAnalyzer.Domain.Model;

namespace BS.ReceiptAnalyzer.Local.Core
{
    public interface IAnalysisTaskManager
    {
        bool IsOnProcessing { get; }
        AnalysisTask? CurrentTask { get; }

        Task<TaskManagerContract.CreateTaskResult> CreateTask(TaskManagerContract.CreateTaskRequest request);
        string GetSourceImagePath(Guid taskId);
    }
}

