using BS.ReceiptAnalyzer.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.ReceiptAnalyzer.Local.Core
{
    public static class TaskManagerContract
    {
        public sealed record CreateTaskRequest
        {
            public string FilePath { get; init; }
        }

        public sealed record CreateTaskResult
        {
            public bool Success { get; init; }
            public string? Error { get; init; }
            public Guid TaskId { get; init; }
        }

        public sealed record GetTaskStateResult
        {

        }
    }
}
