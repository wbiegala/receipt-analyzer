using BS.ReceiptAnalyzer.Data;
using BS.ReceiptAnalyzer.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BS.ReceiptAnalyzer.Core.Internal.Queries
{
    internal class GetAnalysisTaskByImageHashQueryHandler : IRequestHandler<GetAnalysisTaskByImageHashQuery, AnalysisTask?>
    {
        private readonly ReceiptAnalyzerDbContext _dbContext;

        public GetAnalysisTaskByImageHashQueryHandler(ReceiptAnalyzerDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<AnalysisTask?> Handle(GetAnalysisTaskByImageHashQuery query, CancellationToken cancellationToken)
        {
            return await _dbContext.Tasks.AsNoTracking()
                .Where(t => t.ImageHash == query.ImageHash)
                .Where(t => t.Status != AnalysisTaskStatus.Failed && t.Status != AnalysisTaskStatus.Canceled)
                .OrderByDescending(t => t.StartTime)
                .FirstOrDefaultAsync();
        }
    }
}
