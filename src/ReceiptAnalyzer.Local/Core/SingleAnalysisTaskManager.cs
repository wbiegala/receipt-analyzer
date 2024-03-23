using BS.ReceiptAnalyzer.Domain.Model;
using BS.ReceiptAnalyzer.Local.Core.Storage;

namespace BS.ReceiptAnalyzer.Local.Core
{
    internal class SingleAnalysisTaskManager : IAnalysisTaskManager
    {
        private readonly IStorageFacade _storage;
        private AnalysisTask? _current;

        public SingleAnalysisTaskManager(IStorageFacade storage)
        {
            _storage = storage;
        }

        public AnalysisTask? CurrentTask => _current;
        public bool IsOnProcessing => _current != null;

        public async Task<TaskManagerContract.CreateTaskResult> CreateTask(TaskManagerContract.CreateTaskRequest request)
        {
            var taskId = Guid.NewGuid();
            var saveResult = await _storage.SaveSourceImage(request.FilePath, taskId);
            if (saveResult.Item1 != true)
                return new TaskManagerContract.CreateTaskResult { Success = false, Error = saveResult.Item2 };

            var task = AnalysisTask.Create("ignore");
            GenerateTaskId(task, taskId);

            _current = task;

            return new TaskManagerContract.CreateTaskResult { Success = true, TaskId = taskId };
        }

        public string GetSourceImagePath(Guid taskId)
        {
            return _storage.GetSourceImagePath(taskId);
        }

        private void GenerateTaskId(AnalysisTask task, Guid id)
        {
            var taskType = task.GetType();
            var property = taskType.GetProperty(nameof(AnalysisTask.Id));
            property!.SetValue(task, id);
        }
    }
}
