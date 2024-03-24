namespace BS.ReceiptAnalyzer.Local.Core
{
    public static class TaskManagerContract
    {
        public static class CreateTask
        {
            public sealed record Request
            {
                public string FilePath { get; init; }
            }

            public sealed record Result
            {
                public bool Success { get; init; }
                public string? Error { get; init; }
                public Guid TaskId { get; init; }
            }
        }

        public static class ExecuteNextStep
        {
            public sealed record Request
            {
                public Guid TaskId { get; init; }
            }

            public sealed record Result
            {
                public Guid TaskId { get; init; }
                public bool Success { get; init; }
                public bool CanContinue { get; init; }
                public string? Error { get; init; }
                public int CompletionPercentage { get; init; }
                public string StepName { get; init; }
            }
        }
    }
}
