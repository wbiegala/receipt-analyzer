using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.ReceiptAnalyzer.ReceiptPartitioner.Core
{
    public class ReceiptPartitionerServiceContract
    {
        public sealed record Result
        {
            public DateTimeOffset FinishedAt { get; init; }
            public bool IsSuccess { get; init; }
            public string? FailReason { get; init; }
            public int ReceiptsFound { get; init; }
        }
    }
}
