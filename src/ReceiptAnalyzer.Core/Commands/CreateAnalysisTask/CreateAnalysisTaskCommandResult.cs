namespace BS.ReceiptAnalyzer.Core.Commands.CreateAnalysisTask
{
    public sealed record CreateAnalysisTaskCommandResult : CommandResultBase
    {
        public Guid TaskId { get; init; }
        public string Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? StartedAt { get; set; }
        public bool IsNew { get; init; }

        internal static CreateAnalysisTaskCommandResult CreateSuccess(Guid id, string status,
            DateTimeOffset createdAt, DateTimeOffset? startedAt, bool isNew) =>
            new CreateAnalysisTaskCommandResult
            {
                Success = true,
                TaskId = id,
                Status = status,
                CreatedAt = createdAt,
                StartedAt = startedAt,
                IsNew = isNew
            };
    }
}
