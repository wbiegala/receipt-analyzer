using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.ReceiptAnalyzer.ReceiptPartitioner.Core
{
    internal class ReceiptPartitionerService : IReceiptPartitionerService
    {
        public ReceiptPartitionerService()
        {
            
        }

        public async Task<ReceiptPartitionerServiceContract.Result> PartitionReceiptsAsync(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}
