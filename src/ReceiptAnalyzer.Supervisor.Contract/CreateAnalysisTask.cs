﻿namespace BS.ReceiptAnalyzer.Supervisor.Contract
{
    public static class CreateAnalysisTask
    {
        public class Query
        {
            public bool Force { get; set; } = false;
        }

        public class Response
        {
            public Guid TaskId { get; set; }
            public string Status { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
            public DateTimeOffset? StartedAt { get; set; }
            public bool IsNew { get; set; }
        }
    }
}
